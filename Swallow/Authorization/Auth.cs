using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Swallow.Models;
using Swallow.Services;

namespace Swallow.Authorization;
    public class Auth : IJwtAuth
    {
        private readonly string key;

        private readonly UserService _userService;

        public Auth(string key, UserService userService)
        {
            this.key = key;
            this._userService = userService;
        }

        public string Authentication(string email, string password)
        {
            
            User? user = _userService.CredCheckAsync(email, password).Result;

            if(user == null)
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
                        new Claim(ClaimTypes.Email, email),
                        new Claim(ClaimTypes.Name, user.Id),
                        new Claim(ClaimTypes.Role, user.IsAdmin ? "admin" : "user")
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            // 4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            DateTime now = DateTime.UtcNow;

            user.TokenExpiry = now;

            _userService.UpdateAsync(user.Id, user);

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }

        public bool Validate(string token)
        {
            // TODO: Set to check if token is in a token blacklist and deny otherwise allow request
            if (token == null)
                return false;
            
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            var email = jwtToken.Claims.First(x => x.Type == "email").Value;

            if (email == null)
            {
                return false;
            }
            // Place holder code to be used until black list is implemented
            var isValid = _userService.GetByEmailAsync(email).Result.TokenExpiry.Equals(DateTime.UnixEpoch) ? false : true;

            return isValid;
        }
        
    }
