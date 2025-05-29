using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IeltsWeb.api.DTOs; // Thêm dòng này

namespace IeltsWeb.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionService _service;
    public QuestionsController(IQuestionService service) { _service = service; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuestionDto>>> GetAll()
    {
        var questions = await _service.GetAllAsync();
        var result = questions.Select(q => new QuestionDto(q));
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<QuestionDto>> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(new QuestionDto(item));
    }

    [HttpGet("by-test/{testId}")]
    public async Task<ActionResult<IEnumerable<QuestionDto>>> GetByTestId(int testId)
    {
        var questions = await _service.GetByTestIdAsync(testId);
        var result = questions.Select(q => new QuestionDto(q));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<QuestionDto>> Create(QuestionDto dto)
    {
        var question = new Question
        {
            Id = dto.Id,
            TestId = dto.TestId,
            Content = dto.Content ?? string.Empty,
            QuestionType = dto.QuestionType,
            Skill = dto.Skill,
            MediaUrl = dto.MediaUrl,
            Order = dto.Order
        };
        var created = await _service.CreateAsync(question);
        var result = new QuestionDto(created);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, QuestionDto dto)
    {
        if (id != dto.Id) return BadRequest();
        var question = new Question
        {
            Id = dto.Id,
            TestId = dto.TestId,
            Content = dto.Content,
            QuestionType = dto.QuestionType,
            Skill = dto.Skill,
            MediaUrl = dto.MediaUrl,
            Order = dto.Order
        };
        await _service.UpdateAsync(question);
        var updated = await _service.GetByIdAsync(id);
        if (updated == null) return NotFound();
        var result = new QuestionDto(updated);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}