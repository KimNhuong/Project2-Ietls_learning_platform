using System.ComponentModel.DataAnnotations;

namespace IeltsWeb.api.models;

public class Exam
{
    [Key]
    public int ExamId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
}
