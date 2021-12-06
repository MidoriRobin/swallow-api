using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Swallow.Services;

namespace Swallow.Authorization;
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtAuth jwtAuth;
        private readonly TokenBlacklistService _tokenBlacklistService;
        // private readonly 

        public JwtMiddleware(RequestDelegate next, IJwtAuth jwtAuth, TokenBlacklistService tokenBlacklistService)
        {
            _next = next;
            this.jwtAuth = jwtAuth;
            this._tokenBlacklistService = tokenBlacklistService;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            bool doesExist = await _tokenBlacklistService.DoesTokenExistAsync(token);

            if (!doesExist)
            {

                var isValid = jwtAuth.Validate(token);

                if (isValid)
                {
                    var jwtToken = new JwtSecurityToken(token);

                    var email = jwtToken.Claims.First(x => x.Type == "unique_name").Value;

                    context.Items["User"] = email;

                }

                await _next(context);

                return;
            }

            context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;

        }
        
    }
