using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Swallow.Models;
using Swallow.Services;

namespace Swallow.Authorization;
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        // private readonly 

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtAuth jwtAuth, SwallowContext swallowContext)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var isValid = jwtAuth.Validate(token);

            if (isValid)
            {
                var jwtToken = new JwtSecurityToken(token);

                var email = jwtToken.Claims.First(x => x.Type == "unique_name").Value;

                context.Items["User"] = email;

                await _next(context);

                return;
            } else {
                context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;

            }

        }
        
    }
