namespace IeltsWeb.api.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string? Content { get; set; }
        public string? QuestionType { get; set; }
        public string? Skill { get; set; }
        public string? MediaUrl { get; set; }
        public int Order { get; set; }

        public QuestionDto() { }

        public QuestionDto(models.Question q)
        {
            Id = q.Id;
            TestId = q.TestId;
            Content = q.Content;
            QuestionType = q.QuestionType;
            Skill = q.Skill;
            MediaUrl = q.MediaUrl;
            Order = q.Order;
        }
    }
}
