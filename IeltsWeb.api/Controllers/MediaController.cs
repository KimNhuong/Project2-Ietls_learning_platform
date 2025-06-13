using IeltsWeb.api.DTOs;
using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IeltsWeb.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaController : ControllerBase
{
    private readonly IMediaService _service;
    private readonly IUserService _userService;
    public MediaController(IMediaService service, IUserService userService) { _service = service; _userService = userService; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MediaDto>>> GetAll() 
        => Ok((await _service.GetAllAsync()).Select(m => new MediaDto { Id = m.Id, Url = m.Url, Type = m.Type, Uploader = m.Uploader?.Username }));

    [HttpGet("{id}")]
    public async Task<ActionResult<MediaDto>> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(new MediaDto { Id = item.Id, Url = item.Url, Type = item.Type, Uploader = item.Uploader?.Username });
    }

    [HttpPost]
    public async Task<ActionResult<MediaDto>> Create(MediaCreateDto dto)
    {
        var user = (await _userService.SearchUsersByNameAsync(dto.Uploader ?? "")).FirstOrDefault();
        if (user == null) return BadRequest("Uploader not found");
        var media = new Media { Url = dto.Url, Type = dto.Type, Uploader = user, UploadedBy = user.UserId };
        var created = await _service.CreateAsync(media);
        var result = new MediaDto { Id = created.Id, Url = created.Url, Type = created.Type, Uploader = created.Uploader?.Username };
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MediaCreateDto dto)
    {
        var media = await _service.GetByIdAsync(id);
        if (media == null) return NotFound();
        var user = (await _userService.SearchUsersByNameAsync(dto.Uploader ?? "")).FirstOrDefault();
        if (user == null) return BadRequest("Uploader not found");
        media.Url = dto.Url;
        media.Type = dto.Type;
        media.Uploader = user;
        media.UploadedBy = user.UserId;
        await _service.UpdateAsync(media);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}