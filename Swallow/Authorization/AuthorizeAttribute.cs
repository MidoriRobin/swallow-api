using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Swallow.Models;

namespace Swallow.Authorization;
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<string> _roles;

        public AuthorizeAttribute(params string[] roles)
        {
            _roles = roles ?? new string[] {};
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip auth on anonymous attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

            if (allowAnonymous)
                return;
            
            var user = (User)context.HttpContext.Items["User"];

            if (user == null || (_roles.Any() && _roles.Contains("admin") && !user.IsAdmin))
            {
                context.Result = new JsonResult( new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }

    }