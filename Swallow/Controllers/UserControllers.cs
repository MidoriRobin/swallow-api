using Swallow.Models;
using Swallow.Services;
using Microsoft.AspNetCore.Mvc;
using Swallow.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Swallow.Models.Requests;
using Swallow.Services.Postgres;

namespace Swallow.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    // private readonly UserService _usersService;

    private IUserService _userService; 

    public UserController(IUserService userService) => _userService = userService;

    [Authorize(Roles ="admin")]
    [HttpGet]
    public List<User> Get() => _userService.GetAll().ToList();


    // [Authorize(Roles ="admin")]
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

    // [Authorize(Roles ="admin")]
    // [HttpPost]
    // public async Task<IActionResult> Post(User newUser)
    // {
    //     await _usersService.CreateAsync(newUser);

    //     // ? Seek to understand this a bit better
    //     return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
    // }



    [AllowAnonymous]
    // ? What needs to be done in registration
    [HttpPost("register")]
    public IActionResult Register([FromForm] CreateRequest newUser)
    {

        // Do I need to create an error object?

        /* Dictionary<string, string> result = await _usersService.processRegistration(newUser);

        if (!result["success"].Equals("True"))
        {
            return BadRequest(result["reason"]);
        }

        var userDTO = new UserDTO()
        {
            Id = newUser.Id,
            Name = newUser.Name,
            Username = newUser.Username,
            Email = newUser.Email,
            Occupation = newUser.Occupation
        };

        return CreatedAtAction(nameof(Get), new { id = newUser.Id }, userDTO); */

         _userService.Create(newUser);

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