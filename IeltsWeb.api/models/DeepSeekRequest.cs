using System.ComponentModel.DataAnnotations;

namespace IeltsWeb.api.models;

public class DeepSeekRequest
{
    [Key]
    public int RequestId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Skill { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
    public string? Response { get; set; }
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
}
