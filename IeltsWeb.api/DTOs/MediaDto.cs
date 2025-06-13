namespace IeltsWeb.api.DTOs
{
    public class MediaDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public string? Uploader { get; set; }
    }

    public class MediaCreateDto
    {
        public string Url { get; set; }
        public string Type { get; set; }
        public string? Uploader { get; set; }
    }
}
