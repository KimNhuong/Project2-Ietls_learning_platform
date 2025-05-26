using System;

namespace IeltsWeb.api.models
{
    public class UserBookmark
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? LessonId { get; set; }
        public int? QuestionId { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public Lesson Lesson { get; set; }
        public Question Question { get; set; }

        public UserBookmark()
        {
            User = null!;
            Lesson = null!;
            Question = null!;
        }
    }
}
