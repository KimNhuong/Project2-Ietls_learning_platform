namespace IeltsWeb.api.models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string? Content { get; set; }
        public bool IsCorrect { get; set; }
        public string? Explanation { get; set; }

        public Question? Question { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
    }
}
