using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IeltsWeb.api.models;

public class Question
{
    [Key]
    public int QuestionId { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;

    // Đáp án A, B, C, D
    [Required]
    public string OptionA { get; set; } = string.Empty;
    [Required]
    public string OptionB { get; set; } = string.Empty;
    [Required]
    public string OptionC { get; set; } = string.Empty;
    [Required]
    public string OptionD { get; set; } = string.Empty;

    [Required]
    [MaxLength(1)]
    public string CorrectAnswer { get; set; } = string.Empty; // "A", "B", "C", "D"

    public int ExamId { get; set; }
    [JsonIgnore]
    public Exam Exam { get; set; } = null!;

    // Tham chiếu đến các kết quả thi (nếu cần)
    [JsonIgnore]
    public ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
}
