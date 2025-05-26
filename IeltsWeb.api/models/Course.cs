using System;
using System.Collections.Generic;

namespace IeltsWeb.api.models
{
    public class Course
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public User Creator { get; set; }

        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<UserProgress> UserProgresses { get; set; }

        public Course()
        {
            Description = string.Empty;
            Creator = null!;
            Lessons = new List<Lesson>();
            UserProgresses = new List<UserProgress>();
        }
    }
}
