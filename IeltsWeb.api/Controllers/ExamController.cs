using IeltsWeb.api.models;
using IeltsWeb.api.Services;
using Microsoft.AspNetCore.Mvc;
using IeltsWeb.api.Interfaces;

namespace IeltsWeb.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamsController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExams()
        {
            var exams = await _examService.GetAllExamsAsync();
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExam(int id)
        {
            var exam = await _examService.GetExamByIdAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            return Ok(exam);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Exam>>> SearchExams([FromQuery] string keyword)
        {
            var exams = await _examService.SearchExamsByTitleAsync(keyword);
            return Ok(exams);
        }

        [HttpPost]
        public async Task<ActionResult<Exam>> CreateExam(Exam exam)
        {
            await _examService.CreateExamAsync(exam);
            return CreatedAtAction(nameof(GetExam), new { id = exam.ExamId }, exam);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExam(int id, Exam exam)
        {
            if (id != exam.ExamId)
            {
                return BadRequest();
            }

            await _examService.UpdateExamAsync(exam);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            await _examService.DeleteExamAsync(id);
            return NoContent();
        }
    }
}
