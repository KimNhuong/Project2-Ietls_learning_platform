using Microsoft.AspNetCore.Mvc;
using IeltsWeb.api.models;
using IeltsWeb.api.DTOs;
using IeltsWeb.api.Interfaces;

namespace IeltsWeb.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserAnswersController : ControllerBase
{
    private readonly IUserAnswerService _service;
    public UserAnswersController(IUserAnswerService service) { _service = service; }

    // GET: api/useranswers?questionId=5
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserAnswerDto>>> GetByQuestionId([FromQuery] int questionId)
    {
        var answers = (await _service.GetAllAsync())
            .Where(a => a.QuestionId == questionId)
            .Select(a => new UserAnswerDto
            {
                Id = a.Id,
                UserId = a.UserId,
                QuestionId = a.QuestionId,
                AnswerId = a.AnswerId,
                TextAnswer = a.TextAnswer,
                SubmittedAt = a.SubmittedAt,
                IsMarked = a.IsMarked
            });
        return Ok(answers);
    }

    // POST: api/useranswers
    [HttpPost]
    public async Task<ActionResult<UserAnswerDto>> Create(UserAnswerCreateDto dto)
    {
        var answer = new UserAnswer
        {
            UserId = dto.UserId,
            QuestionId = dto.QuestionId,
            AnswerId = dto.AnswerId,
            TextAnswer = dto.TextAnswer ?? string.Empty,
            SubmittedAt = DateTime.UtcNow,
            IsMarked = dto.IsMarked
        };
        var created = await _service.CreateAsync(answer);
        var result = new UserAnswerDto
        {
            Id = created.Id,
            UserId = created.UserId,
            QuestionId = created.QuestionId,
            AnswerId = created.AnswerId,
            TextAnswer = created.TextAnswer,
            SubmittedAt = created.SubmittedAt,
            IsMarked = created.IsMarked
        };
        return CreatedAtAction(nameof(GetByQuestionId), new { questionId = created.QuestionId }, result);
    }

    // PUT: api/useranswers/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UserAnswerCreateDto dto)
    {
        var answer = await _service.GetByIdAsync(id);
        if (answer == null) return NotFound();
        answer.AnswerId = dto.AnswerId;
        answer.TextAnswer = dto.TextAnswer ?? string.Empty;
        answer.IsMarked = dto.IsMarked;
        await _service.UpdateAsync(answer);
        return NoContent();
    }

    // DELETE: api/useranswers/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
