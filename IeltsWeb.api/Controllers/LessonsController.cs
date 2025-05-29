using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IeltsWeb.api.DTOs;

namespace IeltsWeb.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly ILessonService _service;
    public LessonsController(ILessonService service) { _service = service; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetAll()
    {
        var lessons = await _service.GetAllAsync();
        var result = lessons.Select(l => new LessonDto
        {
            Id = l.Id,
            CourseId = l.CourseId,
            Skill = l.Skill,
            Title = l.Title,
            Content = l.Content,
            Order = l.Order
        });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LessonDto>> GetById(int id)
    {
        var l = await _service.GetByIdAsync(id);
        if (l == null) return NotFound();
        var dto = new LessonDto
        {
            Id = l.Id,
            CourseId = l.CourseId,
            Skill = l.Skill,
            Title = l.Title,
            Content = l.Content,
            Order = l.Order
        };
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<LessonDto>> Create(LessonCreateDto dto)
    {
        var lesson = new Lesson
        {
            CourseId = dto.CourseId,
            Skill = dto.Skill,
            Title = dto.Title,
            Content = dto.Content,
            Order = dto.Order
        };
        var created = await _service.CreateAsync(lesson);
        var result = new LessonDto
        {
            Id = created.Id,
            CourseId = created.CourseId,
            Skill = created.Skill,
            Title = created.Title,
            Content = created.Content,
            Order = created.Order
        };
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, LessonUpdateDto dto)
    {
        var lesson = await _service.GetByIdAsync(id);
        if (lesson == null) return NotFound();
        lesson.CourseId = dto.CourseId;
        lesson.Skill = dto.Skill;
        lesson.Title = dto.Title;
        lesson.Content = dto.Content;
        lesson.Order = dto.Order;
        await _service.UpdateAsync(lesson);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("by-course/{courseId}")]
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetByCourseId(int courseId)
    {
        var lessons = await _service.GetByCourseIdAsync(courseId);
        var result = lessons.Select(l => new LessonDto
        {
            Id = l.Id,
            CourseId = l.CourseId,
            Skill = l.Skill,
            Title = l.Title,
            Content = l.Content,
            Order = l.Order
        });
        return Ok(result);
    }
}