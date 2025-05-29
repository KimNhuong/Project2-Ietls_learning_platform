using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IeltsWeb.api.DTOs;

namespace IeltsWeb.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestsController : ControllerBase
{
    private readonly ITestService _service;
    public TestsController(ITestService service) { _service = service; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TestDto>>> GetAll()
    {
        var tests = await _service.GetAllAsync();
        var result = tests.Select(t => new TestDto
        {
            Id = t.Id,
            LessonId = t.LessonId,
            Title = t.Title,
            Description = t.Description,
            Skill = t.Skill,
            Duration = t.Duration
        });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TestDto>> GetById(int id)
    {
        var t = await _service.GetByIdAsync(id);
        if (t == null) return NotFound();
        var dto = new TestDto
        {
            Id = t.Id,
            LessonId = t.LessonId,
            Title = t.Title,
            Description = t.Description,
            Skill = t.Skill,
            Duration = t.Duration
        };
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<TestDto>> Create(TestCreateDto dto)
    {
        var test = new Test
        {
            LessonId = dto.LessonId,
            Title = dto.Title,
            Description = dto.Description,
            Skill = dto.Skill,
            Duration = dto.Duration
        };
        var created = await _service.CreateAsync(test);
        var result = new TestDto
        {
            Id = created.Id,
            LessonId = created.LessonId,
            Title = created.Title,
            Description = created.Description,
            Skill = created.Skill,
            Duration = created.Duration
        };
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TestUpdateDto dto)
    {
        var test = await _service.GetByIdAsync(id);
        if (test == null) return NotFound();
        test.LessonId = dto.LessonId;
        test.Title = dto.Title;
        test.Description = dto.Description;
        test.Skill = dto.Skill;
        test.Duration = dto.Duration;
        await _service.UpdateAsync(test);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("by-lesson/{lessonId}")]
    public async Task<ActionResult<IEnumerable<TestDto>>> GetByLessonId(int lessonId)
    {
        var tests = await _service.GetByLessonIdAsync(lessonId);
        var result = tests.Select(t => new TestDto
        {
            Id = t.Id,
            LessonId = t.LessonId,
            Title = t.Title,
            Description = t.Description,
            Skill = t.Skill,
            Duration = t.Duration
        });
        return Ok(result);
    }
}