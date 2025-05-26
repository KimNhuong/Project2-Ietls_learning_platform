using System;

namespace IeltsWeb.api.models
{
    public class UserProgress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int LessonId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public double? Score { get; set; }

        public User User { get; set; }
        public Course Course { get; set; }
        public Lesson Lesson { get; set; }

        public UserProgress()
        {
            User = null!;
            Course = null!;
            Lesson = null!;
        }
    }
}
