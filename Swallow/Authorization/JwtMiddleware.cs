using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
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

                var id = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;

                context.Items["User"] = swallowContext.Users.Find(id);

                await _next(context);
                return;
            }
            await _next(context);

        }
        
    }
