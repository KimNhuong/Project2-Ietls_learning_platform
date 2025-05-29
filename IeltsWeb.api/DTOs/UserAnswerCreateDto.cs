namespace IeltsWeb.api.DTOs;

public class UserAnswerCreateDto
{
    public int UserId { get; set; }
    public int QuestionId { get; set; }
    public int? AnswerId { get; set; }
    public string? TextAnswer { get; set; }
    public bool IsMarked { get; set; }
}
