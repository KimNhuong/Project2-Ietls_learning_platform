namespace IeltsWeb.api.DTOs;

public class AnswerDto
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string? Content { get; set; }
    public string? IsCorrect { get; set; }
    public string? Explanation { get; set; }
}
