using System;

namespace IeltsWeb.api.models
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public string TextAnswer { get; set; }
        public DateTime SubmittedAt { get; set; }
        public bool IsMarked { get; set; }
        public double? Score { get; set; }

        public User User { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }

        public UserAnswer()
        {
            TextAnswer = string.Empty;
            User = null!;
            Question = null!;
            Answer = null!;
        }
    }
}
