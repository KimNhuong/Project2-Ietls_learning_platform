using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IeltsWeb.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserBookmarksController : ControllerBase
{
    private readonly IUserBookmarkService _service;
    public UserBookmarksController(IUserBookmarkService service) { _service = service; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserBookmark>>> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<UserBookmark>> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<UserBookmark>> Create(UserBookmark bookmark)
    {
        var created = await _service.CreateAsync(bookmark);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UserBookmark bookmark)
    {
        if (id != bookmark.Id) return BadRequest();
        await _service.UpdateAsync(bookmark);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
} 