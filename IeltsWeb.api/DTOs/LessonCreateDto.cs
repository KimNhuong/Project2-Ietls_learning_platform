namespace IeltsWeb.api.DTOs
{
    public class LessonCreateDto
    {
        public int CourseId { get; set; }
        public string? Skill { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int Order { get; set; }
    }
}
