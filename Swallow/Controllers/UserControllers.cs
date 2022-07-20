using Swallow.Models;
using Swallow.Services;
using Microsoft.AspNetCore.Mvc;
using Swallow.Models.DTOs;
using Swallow.Models.Requests;
using Swallow.Services.Postgres;
using Swallow.Authorization;

namespace Swallow.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    // private readonly UserService _usersService;

    private IUserService _userService; 

    public UserController(IUserService userService) => _userService = userService;

    [Authorize("admin")]
    [HttpGet]
    public List<User> Get() => _userService.GetAll().ToList();


    // [Authorize("admin")]
    // [HttpGet("{id:length(24)}")]
    // public async Task<ActionResult<User>> Get(string id)
    // {
    //     var user = await _usersService.GetAsync(id);

    //     if (user is null)
    //     {
    //         return NotFound();
    //     }

    //     return user;
    // }

    [Authorize("admin")]
    [HttpPost]
    public async Task<IActionResult> Post(CreateRequest newUser)
    {
        try
        {
            _userService.Create(newUser);
            
        }
        catch (System.Exception)
        {
            
            throw;
        }

        // ? Seek to understand this a bit better
        // return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        return Ok(new { message = "User created" });
    }



    [AllowAnonymous]
    // ? What needs to be done in registration
    [HttpPost("register")]
    public IActionResult Register([FromForm] CreateRequest newUser)
    {
        newUser.IsAdmin = "false";
        try
        {
            _userService.Create(newUser);
            
        }
        catch (ApplicationException e)
        {
            
            return BadRequest(e.Message);
        }

        return Ok(new { message = "User created" });
    }


    // [Authorize(Roles ="admin")]
    // [HttpPut("{id:length(24)}")]
    // public async Task<IActionResult> Update(string id, User updatedUser)
    // {
    //     var user = await _usersService.GetAsync(id);

    //     if (user is null)
    //     {
    //         return NotFound();
    //     }

    //     updatedUser.Id = user.Id;

    //     await _usersService.UpdateAsync(id, updatedUser);

    //     return NoContent();
    // }


    // [Authorize(Roles ="admin")]
    // [HttpDelete("{id:length(24)}")]
    // public async Task<IActionResult> Delete(string id)
    // {
    //     var user = await _usersService.GetAsync(id);

    //     if (user is null)
    //     {
    //         return NotFound();
    //     }

    //     // ? look into warning 
    //     await _usersService.RemoveAsync(user.Id);

    //     return NoContent();
    // }

}