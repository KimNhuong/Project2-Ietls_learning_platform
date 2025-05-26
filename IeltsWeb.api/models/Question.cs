using System.Collections.Generic;

namespace IeltsWeb.api.models
{
    public class Question
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Content { get; set; }
        public string QuestionType { get; set; }
        public string Skill { get; set; }
        public string MediaUrl { get; set; }
        public int Order { get; set; }

        public Test Test { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; }
        public ICollection<QuestionMedia> QuestionMedias { get; set; }
        public ICollection<UserBookmark> UserBookmarks { get; set; }

        public Question()
        {
            Content = string.Empty;
            QuestionType = string.Empty;
            Skill = string.Empty;
            MediaUrl = string.Empty;
            Test = null!;
            Answers = new List<Answer>();
            UserAnswers = new List<UserAnswer>();
            QuestionMedias = new List<QuestionMedia>();
            UserBookmarks = new List<UserBookmark>();
        }
    }
}
