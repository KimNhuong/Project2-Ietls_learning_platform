using Microsoft.AspNetCore.Mvc;
using IeltsWeb.api.Services;
using System.Threading.Tasks;

namespace IeltsWeb.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly DeepSeekService _deepSeekService;
    public CoursesController(DeepSeekService deepSeekService)
    {
        _deepSeekService = deepSeekService;
    }

    // GET: /api/courses
    [HttpGet]
    public IActionResult GetCourses()
    {
        // TODO: Lấy danh sách khóa học từ DB
        return Ok(new[] { new { CourseId = 1, Name = "Demo Course" } });
    }

    // GET: /api/courses/{id}
    [HttpGet("{id}")]
    public IActionResult GetCourseDetail(int id)
    {
        // TODO: Lấy chi tiết 1 khóa học từ DB
        return Ok(new { CourseId = id, Name = "Demo Course", Description = "Demo" });
    }

    // GET: /api/courses/{courseId}/lessons
    [HttpGet("{courseId}/lessons")]
    public IActionResult GetLessons(int courseId)
    {
        // TODO: Lấy danh sách bài học trong khóa học
        return Ok(new[] { new { LessonId = 1, CourseId = courseId, Title = "Lesson 1" } });
    }

    // GET: /api/lessons/{lessonId}
    [HttpGet("/api/lessons/{lessonId}")]
    public IActionResult GetLessonDetail(int lessonId)
    {
        // TODO: Lấy chi tiết bài học
        return Ok(new { LessonId = lessonId, Title = "Lesson 1", Content = "Demo content" });
    }

    // GET: /api/lessons/{lessonId}/questions
    [HttpGet("/api/lessons/{lessonId}/questions")]
    public IActionResult GetQuestions(int lessonId)
    {
        // TODO: Lấy danh sách câu hỏi trắc nghiệm của bài học
        return Ok(new[] { new { QuestionId = 1, LessonId = lessonId, Content = "What is ...?" } });
    }

    // GET: /api/questions/{questionId}
    [HttpGet("/api/questions/{questionId}")]
    public IActionResult GetQuestionDetail(int questionId)
    {
        // TODO: Lấy chi tiết câu hỏi trắc nghiệm
        return Ok(new { QuestionId = questionId, Content = "What is ...?", Options = new[] { "A", "B", "C" } });
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
}
