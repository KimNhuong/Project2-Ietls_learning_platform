using System;

namespace IeltsWeb.api.models
{
    public class TestAttempt
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public double? TotalScore { get; set; }

        public User User { get; set; }
        public Test Test { get; set; }

        public TestAttempt()
        {
            User = null!;
            Test = null!;
        }
    }
}
