using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IeltsWeb.api.DTOs;
using System.Linq;

namespace IeltsWeb.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionMediasController : ControllerBase
{
    private readonly IQuestionMediaService _service;
    public QuestionMediasController(IQuestionMediaService service) { _service = service; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuestionMediaDto>>> GetAll() 
        => Ok((await _service.GetAllAsync()).Select(x => new QuestionMediaDto { QuestionId = x.QuestionId, MediaId = x.MediaId }));

    [HttpGet("{questionId}/{mediaId}")]
    public async Task<ActionResult<QuestionMediaDto>> GetById(int questionId, int mediaId)
    {
        var item = await _service.GetByIdAsync(questionId, mediaId);
        if (item == null) return NotFound();
        return Ok(new QuestionMediaDto { QuestionId = item.QuestionId, MediaId = item.MediaId });
    }

    [HttpPost]
    public async Task<ActionResult<QuestionMediaDto>> Create(QuestionMediaCreateDto dto)
    {
        var entity = new QuestionMedia { QuestionId = dto.QuestionId, MediaId = dto.MediaId };
        var created = await _service.CreateAsync(entity);
        var result = new QuestionMediaDto { QuestionId = created.QuestionId, MediaId = created.MediaId };
        return CreatedAtAction(nameof(GetById), new { questionId = result.QuestionId, mediaId = result.MediaId }, result);
    }

    [HttpDelete("{questionId}/{mediaId}")]
    public async Task<IActionResult> Delete(int questionId, int mediaId)
    {
        await _service.DeleteAsync(questionId, mediaId);
        return NoContent();
    }
}