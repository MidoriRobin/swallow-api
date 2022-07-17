using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swallow.Models;

namespace Swallow.Authorization;
    public class Auth : IJwtAuth
    {
        private readonly string key;
        private readonly SwallowContext _context;

        // private readonly UserService _userService;

        public Auth(SwallowContext context)
        {
            this.key = Environment.GetEnvironmentVariable("JWT__SECRET");
            // this._userService = userService;
            this._context = context;
        }

        //TODO: build out service, test, refactor controller
        public string Authentication(User userInfo)
        {

            if(userInfo == null)
            {
                return null;
            }

            // 1. Create security token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create private key to encrypt
            var tokenKey = Encoding.ASCII.GetBytes(key);

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
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            // 4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            DateTime nowPlusHour = DateTime.UtcNow.AddHours(1);

            userInfo.TokenExpiry = nowPlusHour;

            _context.Users.Update(userInfo);
            _context.SaveChanges();

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
            // return token;
        }

        
        /// <summary>
        /// Checks if a token is not black listed and not expired
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public int? Validate(string token)
        {
            int userId;
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
            }
            catch (System.Exception)
            {
                
                return null;
            }

            return userId;
        }
        
        
    }
