using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IeltsWeb.api.DTOs;

namespace IeltsWeb.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnswersController : ControllerBase
{
    private readonly IAnswerService _service;
    public AnswersController(IAnswerService service) { _service = service; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AnswerDto>>> GetAll()
    {
        var answers = (await _service.GetAllAsync()).Select(a => new AnswerDto
        {
            Id = a.Id,
            QuestionId = a.QuestionId,
            Content = a.Content,
            IsCorrect = a.IsCorrect,
            Explanation = a.Explanation
        });
        return Ok(answers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AnswerDto>> GetById(int id)
    {
        var a = await _service.GetByIdAsync(id);
        if (a == null) return NotFound();
        var dto = new AnswerDto
        {
            Id = a.Id,
            QuestionId = a.QuestionId,
            Content = a.Content,
            IsCorrect = a.IsCorrect,
            Explanation = a.Explanation
        };
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<AnswerDto>> Create(AnswerDto dto)
    {
        var answer = new Answer
        {
            QuestionId = dto.QuestionId,
            Content = dto.Content,
            IsCorrect = dto.IsCorrect,
            Explanation = dto.Explanation
        };
        var created = await _service.CreateAsync(answer);
        var result = new AnswerDto
        {
            Id = created.Id,
            QuestionId = created.QuestionId,
            Content = created.Content,
            IsCorrect = created.IsCorrect,
            Explanation = created.Explanation
        };
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AnswerDto dto)
    {
        if (id != dto.Id) return BadRequest();
        var answer = await _service.GetByIdAsync(id);
        if (answer == null) return NotFound();
        answer.QuestionId = dto.QuestionId;
        answer.Content = dto.Content;
        answer.IsCorrect = dto.IsCorrect;
        answer.Explanation = dto.Explanation;
        await _service.UpdateAsync(answer);
        var updated = await _service.GetByIdAsync(id);
        if (updated == null) return NotFound();
        var result = new AnswerDto
        {
            Id = id,
            QuestionId = dto.QuestionId,
            Content = dto.Content,
            IsCorrect = dto.IsCorrect,
            Explanation = dto.Explanation
        };
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}