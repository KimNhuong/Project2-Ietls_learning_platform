using System.ComponentModel.DataAnnotations;

namespace IeltsWeb.api.models;

public class SkillResult
{
    [Key]
    public int ResultId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int ExerciseId { get; set; }
    public SkillExercise SkillExercise { get; set; } = null!;

    public int Score { get; set; }
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
}
