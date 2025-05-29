using Microsoft.AspNetCore.Mvc;
using IeltsWeb.api.Services;
using System.Threading.Tasks;
using IeltsWeb.api.models; // Thêm using cho MyDbContext
using IeltsWeb.api.DTOs;

namespace IeltsWeb.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly DeepSeekService _deepSeekService;
    private readonly MyDbContext _context;
    public CoursesController(MyDbContext context, DeepSeekService deepSeekService)
    {
        _context = context;
        _deepSeekService = deepSeekService;
    }

    // GET: /api/courses
    [HttpGet]
    public IActionResult GetCourses()
    {
        var courses = _context.Courses.ToList();
        return Ok(courses);
    }

    // GET: /api/courses/{id}
    [HttpGet("{id}")]
    public IActionResult GetCourseDetail(int id)
    {
        var course = _context.Courses.Find(id);
        if (course == null) return NotFound();
        return Ok(course);
    }

    // POST: /api/courses
    [HttpPost]
    public IActionResult CreateCourse([FromBody] CourseCreateDto dto)
    {
        var newCourse = new Course
        {
            Title = dto.Title ?? string.Empty,
            Description = dto.Description ?? string.Empty,
            CreatedAt = dto.CreatedAt,
            CreatedBy = dto.CreatedBy,
            Rating = (int)dto.Rating,
            Level = dto.Level ?? string.Empty,
            ImageUrl = dto.ImageUrl ?? string.Empty
        };
        _context.Courses.Add(newCourse);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetCourseDetail), new { id = newCourse.Id }, newCourse);
    }

    // PUT: /api/courses/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateCourse(int id, [FromBody] CourseUpdateDto dto)
    {
        var existing = _context.Courses.Find(id);
        if (existing == null) return NotFound();
        existing.Title = dto.Title;
        existing.Description = dto.Description;
        existing.CreatedAt = dto.CreatedAt;
        existing.CreatedBy = dto.CreatedBy;
        existing.Rating = dto.Rating;
        existing.Level = dto.Level;
        existing.ImageUrl = dto.ImageUrl;
        _context.SaveChanges();
        return NoContent();
    }

    // DELETE: /api/courses/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteCourse(int id)
    {
        var course = _context.Courses.Find(id);
        if (course == null) return NotFound();
        _context.Courses.Remove(course);
        _context.SaveChanges();
        return NoContent();
    }

    // Ví dụ endpoint sử dụng DeepSeek AI
    [HttpPost("deepseek-demo")]
    public async Task<IActionResult> DeepSeekDemo([FromBody] string prompt)
    {
        try
        {
            var result = await _deepSeekService.CallDeepSeekAsync(prompt);
            return Ok(result);
        }
        catch (HttpRequestException ex)
        {
            // Log the exception if needed
            return StatusCode(502, $"DeepSeek service error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Log the exception if needed
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: /api/courses/search?title=abc
    [HttpGet("search")]
    public IActionResult GetCoursesByTitle([FromQuery] string title)
    {
        var courses = _context.Courses.Where(c => c.Title != null && c.Title.Contains(title)).ToList();
        return Ok(courses);
    }
}
