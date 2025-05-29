namespace IeltsWeb.api.DTOs;

public class UserAnswerDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int QuestionId { get; set; }
    public int? AnswerId { get; set; }
    public string? TextAnswer { get; set; }
    public DateTime SubmittedAt { get; set; }
    public bool IsMarked { get; set; }
}
