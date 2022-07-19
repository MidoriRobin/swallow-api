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

            // var isTokenBlacklisted = swallowContext.TokenBlacklist.Where(x => x.Token == token).FirstOrDefault();

            // if (isTokenBlacklisted is not null)
            // {
            //     await _next(context);
            //     return;
            // }

            var userId = jwtAuth.Validate(token);

            if (userId is not null)
            {
                context.Items["User"] = swallowContext.Users.Find(userId);

                await _next(context);
                return;
            }
            await _next(context);

        }
        
    }
