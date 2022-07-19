using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Swallow.Models;
using Swallow.Models.Responses;

namespace Swallow.Authorization;
    public class Auth : IJwtAuth
    {
        private readonly string key;
        private readonly SwallowContext _context;

        private readonly IMapper _mapper;

        // private readonly UserService _userService;

        public Auth(SwallowContext context, IMapper mapper)
        {
            this.key = Environment.GetEnvironmentVariable("JWT__SECRET");
            // this._userService = userService;
            this._context = context;
            this._mapper = mapper;
        }

        //TODO: build out service, test, refactor controller
        public AuthenticateResponse Authentication(User userInfo, string ipAddress)
        {

            if(userInfo == null)
            {
                return null;
            }

            // 1. Create security token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create private key to encrypt
            var tokenKey = Encoding.ASCII.GetBytes(key);

            var expiry = DateTime.UtcNow.AddHours(1);

            // 3. Create JWTdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Email, userInfo.Email),
                        new Claim("id", userInfo.Id.ToString()),
                        new Claim(ClaimTypes.Role, userInfo.IsAdmin ? "admin" : "userInfo")
                    }),
                Expires = expiry,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            // 4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Create refresh
            var refreshToken = GenerateRefreshToken(ipAddress);

            userInfo.TokenExpiry = tokenDescriptor.Expires;

            userInfo.RefreshTokens.Add(refreshToken);
            
            removeOldRefreshTokens(userInfo);

            _context.Users.Update(userInfo);
            _context.SaveChanges();
            
            var response = _mapper.Map<AuthenticateResponse>(userInfo);

            response.JwtToken = tokenHandler.WriteToken(token);
            response.RefreshToken = refreshToken.Token;



            // 5. Return Response
            return response;
        }

        
        /// <summary>
        /// Checks if a token is not black listed and not expired
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public int? Validate(string token)
        {
            int userId;
            DateTime expiry;
            // TODO: Set to check if token is in a token blacklist and deny otherwise allow request

            if (token == null)
               return null;
            

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters 
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out SecurityToken validatedToken);


                var jwtToken = (JwtSecurityToken)validatedToken;
                userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);


                // Checks token expiry and throws an app exception if the token does not match current token timestamp in db
                expiry = (DateTime)validatedToken.ValidTo;

                DateTime userExpiry = (DateTime)_context.Users.Find(userId).TokenExpiry;

                bool match = DateTime.Equals(expiry, Truncate(userExpiry, TimeSpan.TicksPerSecond));

                if (!match)
                {
                    throw new ApplicationException("Token mismatch");
                }
            }
            catch (System.Exception)
            {
                
                return null;
            }
            

            return userId;
        }

        public void RevokeRefreshToken(string token)
        {

        }

        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            RefreshToken refreshToken = new RefreshToken 
            {
                Token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddHours(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };

            var tokenIsUnique = !_context.Users.Any(u => u.RefreshTokens.Any(t => t.Token == refreshToken.Token));

            if(!tokenIsUnique)
                return GenerateRefreshToken(ipAddress);

            return refreshToken;
        }

        private void removeOldRefreshTokens(User user)
        {
            user.RefreshTokens.RemoveAll(x => !x.IsActive && x.Created.AddDays(3) <= DateTime.UtcNow);
        }

        private DateTime Truncate(DateTime date, long resolution)
        {
            return new DateTime(date.Ticks - (date.Ticks % resolution), date.Kind);
        }

    }
