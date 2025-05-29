namespace IeltsWeb.api.DTOs
{
    public class CourseCreateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int Rating { get; set; }
        public string? Level { get; set; }
        public string? ImageUrl { get; set; }
    }
}
