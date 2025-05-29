namespace IeltsWeb.api.DTOs
{
    public class CourseUpdateDto
    {
        public string? Title { get; set; }
        public required string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int Rating { get; set; }
        public required string Level { get; set; }
        public required string ImageUrl { get; set; }
    }
}
