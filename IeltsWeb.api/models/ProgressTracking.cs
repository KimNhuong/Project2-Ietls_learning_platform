using System.ComponentModel.DataAnnotations;

namespace IeltsWeb.api.models;

public class ProgressTracking
{
    [Key]
    public int ProgressId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Skill { get; set; } = string.Empty;

    public double ProcessPercentage { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}
