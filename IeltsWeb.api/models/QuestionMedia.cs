namespace IeltsWeb.api.models
{
    public class QuestionMedia
    {
        public int QuestionId { get; set; }
        public int MediaId { get; set; }

        public Question Question { get; set; }
        public Media Media { get; set; }

        public QuestionMedia()
        {
            Question = null!;
            Media = null!;
        }
    }
}
