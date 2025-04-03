// Tạo file Controllers/UsersController.cs
using IeltsWeb.api.models;
using IeltsWeb.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace IeltsWeb.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
  private readonly IUserService _userService;

  public UsersController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<User>>> GetUsers()
  {
    var users = await _userService.GetAllUsersAsync();
    return Ok(users);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<User>> GetUser(int id)
  {
    var user = await _userService.GetUserByIdAsync(id);

    if (user == null)
    {
      return NotFound();
    }

    return Ok(user);
  }

  [HttpPost]
  public async Task<ActionResult<User>> CreateUser(User user)
  {
    await _userService.CreateUserAsync(user);
    return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateUser(int id, User user)
  {
    if (id != user.UserId)
    {
      return BadRequest();
    }

    await _userService.UpdateUserAsync(user);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteUser(int id)
  {
    await _userService.DeleteUserAsync(id);
    return NoContent();
  }

  [HttpGet("search")]
  public async Task<ActionResult<IEnumerable<User>>> SearchUsersByName([FromQuery] string name)
  {
    var users = await _userService.SearchUsersByNameAsync(name);
    if (!users.Any())
    {
      return NotFound(new { Message = "No users found matching the search criteria." });
    }
    return Ok(users);
  }
}