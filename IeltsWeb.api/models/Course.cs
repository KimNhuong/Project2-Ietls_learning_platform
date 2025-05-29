using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IeltsWeb.api.models
{
    public class Course
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }
        public int Rating { get; set; } // 1-5 star
        public string Level { get; set; } // B1, B2, C1, C2...
        public string ImageUrl { get; set; }

        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<UserProgress> UserProgresses { get; set; }

        public Course()
        {
            Description = string.Empty;
            Level = string.Empty;
            Rating = 0;
            ImageUrl = string.Empty;
            Creator = null!;
            Lessons = new List<Lesson>();
            UserProgresses = new List<UserProgress>();
        }
    }
}
