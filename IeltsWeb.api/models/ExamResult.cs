using System.ComponentModel.DataAnnotations;

namespace IeltsWeb.api.models;

public class ExamResult
{
    [Key]
    public int ResultId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int ExamId { get; set; }
    public Exam Exam { get; set; } = null!;

    public int Score { get; set; }
    public DateTime TakenAt { get; set; } = DateTime.UtcNow;
}
