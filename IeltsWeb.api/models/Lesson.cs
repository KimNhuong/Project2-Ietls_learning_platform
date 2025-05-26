using System.Collections.Generic;

namespace IeltsWeb.api.models
{
    public class Lesson
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Skill { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }

        public Course Course { get; set; }
        public ICollection<Test> Tests { get; set; }
        public ICollection<UserProgress> UserProgresses { get; set; }
        public ICollection<UserBookmark> UserBookmarks { get; set; }

        public Lesson()
        {
            Skill = string.Empty;
            Title = string.Empty;
            Content = string.Empty;
            Course = null!;
            Tests = new List<Test>();
            UserProgresses = new List<UserProgress>();
            UserBookmarks = new List<UserBookmark>();
        }
    }
}
