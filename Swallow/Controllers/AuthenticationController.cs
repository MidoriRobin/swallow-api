using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swallow.Models;
using Swallow.Models.DTOs;
using Swallow.Services;

namespace Swallow.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    
    private readonly IJwtAuth jwtAuth;

    public readonly UserService _userService;

    public AuthenticationController(IJwtAuth jwtAuth, UserService userService)
    {
        this.jwtAuth = jwtAuth;
        _userService = userService;
    }

    [HttpGet]
    public async Task<List<User>> AllUsers() => await _userService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<User>> UserById(string id)
    {
        var user = await _userService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return user; 
    }

    [AllowAnonymous]
    [HttpPost("authentication")]
    public IActionResult Authentication([FromBody]UserLoginDTO userCredentials)
    {
        var token = jwtAuth.Authentication(userCredentials.Email, userCredentials.Password);
        
        if(token == null)
            return Unauthorized();
        
        return Ok(token);
    }

    [HttpPost("login")]
    public ActionResult Login()
    {

        return NoContent();
    }


    [HttpPost("logout")]
    public async Task<IActionResult> Logout() => NoContent();

}

