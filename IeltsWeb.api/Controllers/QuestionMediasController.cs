using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IeltsWeb.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionMediasController : ControllerBase
{
    private readonly IQuestionMediaService _service;
    public QuestionMediasController(IQuestionMediaService service) { _service = service; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuestionMedia>>> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{questionId}/{mediaId}")]
    public async Task<ActionResult<QuestionMedia>> GetById(int questionId, int mediaId)
    {
        var item = await _service.GetByIdAsync(questionId, mediaId);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<QuestionMedia>> Create(QuestionMedia entity)
    {
        var created = await _service.CreateAsync(entity);
        return CreatedAtAction(nameof(GetById), new { questionId = created.QuestionId, mediaId = created.MediaId }, created);
    }

    [HttpDelete("{questionId}/{mediaId}")]
    public async Task<IActionResult> Delete(int questionId, int mediaId)
    {
        await _service.DeleteAsync(questionId, mediaId);
        return NoContent();
    }
} 