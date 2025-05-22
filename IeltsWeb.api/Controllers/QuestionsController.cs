using IeltsWeb.api.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IeltsWeb.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class QuestionsController : ControllerBase
{
    private readonly MyDbContext _context;

    public QuestionsController(MyDbContext context)
    {
        _context = context;
    }

    // Thêm câu hỏi cho Exam (giới hạn 40 câu)
    [HttpPost("exam/{examId}")]
    public async Task<IActionResult> AddQuestion(int examId, [FromBody] QuestionCreateDto dto)
    {
        var count = await _context.Questions.CountAsync(q => q.ExamId == examId);
        if (count >= 40)
            return BadRequest(new { message = "Exam can have at most 40 questions." });

        var question = new Question
        {
            Content = dto.Content,
            OptionA = dto.OptionA,
            OptionB = dto.OptionB,
            OptionC = dto.OptionC,
            OptionD = dto.OptionD,
            CorrectAnswer = dto.CorrectAnswer,
            ExamId = examId
        };
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
        return Ok(question);
    }

    // Sửa câu hỏi
    [HttpPut("{questionId}")]
    public async Task<IActionResult> UpdateQuestion(int questionId, [FromBody] QuestionUpdateDto updated)
    {
        var question = await _context.Questions.FindAsync(questionId);
        if (question == null) return NotFound();
        question.Content = updated.Content;
        question.OptionA = updated.OptionA;
        question.OptionB = updated.OptionB;
        question.OptionC = updated.OptionC;
        question.OptionD = updated.OptionD;
        question.CorrectAnswer = updated.CorrectAnswer;
        await _context.SaveChangesAsync();
        return Ok(question);
    }

    // Xóa câu hỏi
    [HttpDelete("{questionId}")]
    public async Task<IActionResult> DeleteQuestion(int questionId)
    {
        var question = await _context.Questions.FindAsync(questionId);
        if (question == null) return NotFound();
        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // Chấm điểm: nhận vào danh sách {questionId, selectedOptionKey}, trả về điểm và đáp án đúng/sai
    [HttpPost("grade/{examId}")]
    public async Task<IActionResult> GradeExam(int examId, [FromBody] List<UserAnswerDto> answers)
    {
        var questions = await _context.Questions
            .Where(q => q.ExamId == examId)
            .ToListAsync();

        int score = 0;
        var result = new List<object>();

        foreach (var q in questions)
        {
            var userAnswer = answers.FirstOrDefault(a => a.QuestionId == q.QuestionId);
            bool isCorrect = userAnswer != null && userAnswer.SelectedOptionKey == q.CorrectAnswer;
            if (isCorrect) score++;
            result.Add(new
            {
                q.QuestionId,
                q.Content,
                q.OptionA,
                q.OptionB,
                q.OptionC,
                q.OptionD,
                CorrectAnswer = q.CorrectAnswer,
                UserAnswer = userAnswer?.SelectedOptionKey,
                IsCorrect = isCorrect
            });
        }

        return Ok(new { Score = score, Total = questions.Count, Details = result });
    }

    // DTOs
    public class QuestionCreateDto
    {
        public string Content { get; set; } = string.Empty;
        public string OptionA { get; set; } = string.Empty;
        public string OptionB { get; set; } = string.Empty;
        public string OptionC { get; set; } = string.Empty;
        public string OptionD { get; set; } = string.Empty;
        public string CorrectAnswer { get; set; } = string.Empty;
    }

    public class QuestionUpdateDto : QuestionCreateDto { }

    public class UserAnswerDto
    {
        public int QuestionId { get; set; }
        public string SelectedOptionKey { get; set; } = "";
    }
}
// Không thay đổi, chỉ giữ controller, inject MyDbContext hoặc IQuestionService nếu tách riêng service cho Question
