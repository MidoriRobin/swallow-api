using System;
using Microsoft.AspNetCore.Authorization;
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

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authentication([FromBody]LoginRequest userCred)
    {
        User? user = _userService.CredCheck(userCred.Email, userCred.Password);

        var token = _userService.Authenticate(user);
        
        if(token == null)
            return Unauthorized("Invalid Credentials");
        
        return Ok(token);
    }

    // // [HttpPost("login")]
    // // public ActionResult Login()
    // // {

    // //     return NoContent();
    // // }


    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var currentUser = HttpContext.User;


        var claims = currentUser.Claims;
        string userEmail = claims.ToArray()[0].Value;

        string userId = claims.ToArray()[1].Value;
        
        string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        DateTime nullDate = DateTime.UnixEpoch;

        User user = _userService.GetById(int.Parse(userId));

        user.TokenExpiry = nullDate;

        _userService.Update(user.Id, user);

        // await _tokenBlacklistService.CreateAsync(new TokenBlacklist(){Token = token, EntryDate = now, BelongsTo = userEmail});

        return NoContent();
    }

}

