using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

    [Range(1, 5)]
    public int Ratings { get; set; } = 1; // Giá trị mặc định là 1 sao

    [MaxLength(500)]
    public string ImageUrl { get; set; } = string.Empty; // URL hình ảnh

    [MaxLength(500)]
    public string VideoUrl { get; set; } = string.Empty; // URL video

    [Required]
    [MaxLength(20)]
    public string Level { get; set; } = "Beginner"; // Giá trị mặc định là Beginner

    [JsonIgnore]
    public ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();

    public ICollection<Question> Questions { get; set; } = new List<Question>(40);
}
