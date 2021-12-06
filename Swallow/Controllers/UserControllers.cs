using Swallow.Models;
using Swallow.Services;
using Microsoft.AspNetCore.Mvc;
using Swallow.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Swallow.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _usersService;

    public UserController(UserService userService) => _usersService = userService;

    [HttpGet]
    public async Task<List<User>> Get() => await _usersService.GetAsync();


    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var user = await _usersService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }


    [HttpPost]
    public async Task<IActionResult> Post(User newUser)
    {
        await _usersService.CreateAsync(newUser);

        // ? Seek to understand this a bit better
        return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
    }



    [AllowAnonymous]
    // ? What needs to be done in registration
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] User newUser)
    {

        // Do I need to create an error object?

        Dictionary<string, string> result = await _usersService.processRegistration(newUser);

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

        return CreatedAtAction(nameof(Get), new { id = newUser.Id }, userDTO);
    }


    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, User updatedUser)
    {
        var user = await _usersService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        updatedUser.Id = user.Id;

        await _usersService.UpdateAsync(id, updatedUser);

        return NoContent();
    }


    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _usersService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        // ? look into warning 
        await _usersService.RemoveAsync(user.Id);

        return NoContent();
    }

}