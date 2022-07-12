using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swallow.Authorization;
using Swallow.Models;
using Swallow.Models.DTOs;
using Swallow.Services;

namespace Swallow.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    
    // private readonly IJwtAuth jwtAuth;

    // public readonly UserService _userService;
    // public readonly TokenBlacklistService _tokenBlacklistService;

    // public AuthenticationController(IJwtAuth jwtAuth, UserService userService, TokenBlacklistService tokenBlacklistService)
    // {
    //     this.jwtAuth = jwtAuth;
    //     _userService = userService;
    //     _tokenBlacklistService = tokenBlacklistService;
    // }

    // [HttpGet]
    // public async Task<List<User>> AllUsers() => await _userService.GetAsync();

    // [HttpGet("{id:length(24)}")]
    // public async Task<ActionResult<User>> UserById(string id)
    // {
    //     var user = await _userService.GetAsync(id);

    //     if (user is null)
    //     {
    //         return NotFound();
    //     }

    //     return user; 
    // }

    // [AllowAnonymous]
    // [HttpPost("authenticate")]
    // public IActionResult Authentication([FromBody]UserLoginDTO userCredentials)
    // {
    //     var token = jwtAuth.Authentication(userCredentials.Email, userCredentials.Password);
        
    //     if(token == null)
    //         return Unauthorized("Invalid Credentials");
        
    //     return Ok(token);
    // }

    // // [HttpPost("login")]
    // // public ActionResult Login()
    // // {

    // //     return NoContent();
    // // }


    // [HttpPost("logout")]
    // public async Task<IActionResult> Logout()
    // {
    //     var currentUser = HttpContext.User;


    //     var claims = currentUser.Claims;
    //     string userEmail = claims.ToArray()[0].Value;


    //     bool didReset = await _userService.ResetTokenDateAsync(userEmail);
        
    //     if (!didReset)
    //         return NotFound();
        
    //     string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

    //     DateTime now = DateTime.UtcNow;

    //     await _tokenBlacklistService.CreateAsync(new TokenBlacklist(){Token = token, EntryDate = now, BelongsTo = userEmail});

    //     return NoContent();
    // }

}

