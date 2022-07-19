using System;
using Microsoft.AspNetCore.Mvc;
using Swallow.Authorization;
using Swallow.Models;
using Swallow.Models.DTOs;
using Swallow.Models.Requests;
using Swallow.Services.Postgres;

namespace Swallow.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    
    private readonly IJwtAuth _jwtAuth;

    private readonly IUserService _userService;
    // public readonly TokenBlacklistService _tokenBlacklistService;

    public AuthenticationController(IJwtAuth jwtAuth, IUserService userService /*,TokenBlacklistService tokenBlacklistService*/)
    {
        this._jwtAuth = jwtAuth;
        _userService = userService;
        // _tokenBlacklistService = tokenBlacklistService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authentication([FromBody]LoginRequest userCred)
    {
        
        User? user = _userService.CredCheck(userCred.Email, userCred.Password);
        
        var isLoggedIn = _userService.IsLoggedIn(user.Id);
        
        if (isLoggedIn)
            return Unauthorized("User is already logged in");
        
        // Creating jwt token

        // var token = _userService.Authenticate(user, ipAddress());

        var authResponse = _jwtAuth.Authentication(user, ipAddress());

        if(authResponse == null)
            return Unauthorized("Invalid Credentials");

        setTokenCookie(authResponse.RefreshToken);
        
        
        // Return refresh token as well

        return Ok(authResponse);
    }

    private string ipAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        else
            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var currentUser = HttpContext.User;


        var claims = currentUser.Claims;
        string userEmail = claims.ToArray()[0].Value;

        string userId = claims.ToArray()[1].Value;
        
        string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        var refreshToken = Request.Cookies["refreshToken"];

        _userService.invalidateToken(userId, token);
        // _userService.RevokeToken(token, ipAddress());

        return NoContent();
    }

    [HttpGet("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        
        string newToken = "token";
        var currentUser = HttpContext.User;


        return Ok(new {token= newToken});
    }

    private void setTokenCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(3)
        };
    }
}

