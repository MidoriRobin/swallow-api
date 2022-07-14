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

            // // 3. Create JWTdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Email, userInfo.Email),
                        new Claim(ClaimTypes.Name, userInfo.Id.ToString()),
                        new Claim(ClaimTypes.Role, userInfo.IsAdmin ? "admin" : "userInfo")
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            // // 4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            DateTime now = DateTime.UtcNow;

            // userInfo.TokenExpiry = now;

            // _userService.UpdateAsync(userInfo.Id, userInfo);

            // // 5. Return Token from method
            // return tokenHandler.WriteToken(token);
            return "token";
        }

        public bool Validate(string token)
        {
            var isValid = false;
            // // TODO: Set to check if token is in a token blacklist and deny otherwise allow request
            // if (token == null)
            //     return false;
            
            // var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            // var email = jwtToken.Claims.First(x => x.Type == "email").Value;

            // if (email == null)
            // {
            //     return false;
            // }
            // // Place holder code to be used until black list is implemented
            // var isValid = _userService.GetByEmailAsync(email).Result.TokenExpiry.Equals(DateTime.UnixEpoch) ? false : true;

            return isValid;
        }
        
    }
