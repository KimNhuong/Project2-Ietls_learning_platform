namespace IeltsWeb.api.DTOs
{
    public class QuestionMediaDto
    {
        public int QuestionId { get; set; }
        public int MediaId { get; set; }
    }

    public class QuestionMediaCreateDto
    {
        public int QuestionId { get; set; }
        public int MediaId { get; set; }
    }
}
