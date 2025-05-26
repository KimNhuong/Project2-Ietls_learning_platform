using System;
using System.Collections.Generic;

namespace IeltsWeb.api.models
{
    public class Test
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Skill { get; set; }
        public int Duration { get; set; } // phút
        public DateTime CreatedAt { get; set; }

        public Lesson Lesson { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<TestAttempt> TestAttempts { get; set; }

        public Test()
        {
            Title = string.Empty;
            Description = string.Empty;
            Skill = string.Empty;
            Lesson = null!;
            Questions = new List<Question>();
            TestAttempts = new List<TestAttempt>();
        }
    }
}
