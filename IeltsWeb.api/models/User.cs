using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IeltsWeb.api.models;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace IeltsWeb.api.models;

public class User 
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string PasswordHash { get; set; } = string.Empty;

    public int RoleId { get; set; }

    [JsonIgnore] // 🚀 Loại bỏ Role khi serialize JSON
    public Role? Role { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ICollection<Course> CoursesCreated { get; set; }
    public ICollection<UserProgress> UserProgresses { get; set; }
    public ICollection<UserAnswer> UserAnswers { get; set; }
    public ICollection<Media> UploadedMedia { get; set; }
    public ICollection<TestAttempt> TestAttempts { get; set; }
    public ICollection<UserBookmark> UserBookmarks { get; set; }

    public User()
    {
        CoursesCreated = new List<Course>();
        UserProgresses = new List<UserProgress>();
        UserAnswers = new List<UserAnswer>();
        UploadedMedia = new List<Media>();
        TestAttempts = new List<TestAttempt>();
        UserBookmarks = new List<UserBookmark>();
    }
}
