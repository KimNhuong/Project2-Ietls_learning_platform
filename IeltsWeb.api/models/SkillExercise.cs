using System.ComponentModel.DataAnnotations;

namespace IeltsWeb.api.models;

public class SkillExercise
{
    [Key]
    public int ExerciseId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Skill { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Difficulty { get; set; } = "Medium";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<SkillResult> SkillResults { get; set; } = new List<SkillResult>();
}
