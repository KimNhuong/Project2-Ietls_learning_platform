namespace IeltsWeb.api.DTOs;

public class TestCreateDto
{
    public int LessonId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Order { get; set; }
    public string? Skill { get; set; }
    public int Duration { get; set; }
    // Thêm các trường khác nếu cần
}
