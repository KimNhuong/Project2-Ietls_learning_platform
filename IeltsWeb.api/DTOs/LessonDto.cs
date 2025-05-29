namespace IeltsWeb.api.DTOs
{
    public class LessonDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? Skill { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int Order { get; set; }

        public LessonDto() { }

        public LessonDto(models.Lesson l)
        {
            Id = l.Id;
            CourseId = l.CourseId;
            Skill = l.Skill;
            Title = l.Title;
            Content = l.Content;
            Order = l.Order;
        }
    }
}
