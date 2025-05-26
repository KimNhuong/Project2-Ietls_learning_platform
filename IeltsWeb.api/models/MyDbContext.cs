using IeltsWeb.api.Models;
using Microsoft.EntityFrameworkCore;

namespace IeltsWeb.api.models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<UserProgress> UserProgresses { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<QuestionMedia> QuestionMedias { get; set; }
        public DbSet<TestAttempt> TestAttempts { get; set; }
        public DbSet<UserBookmark> UserBookmarks { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key for QuestionMedia
            modelBuilder.Entity<QuestionMedia>()
                .HasKey(qm => new { qm.QuestionId, qm.MediaId });

            // ...add more Fluent API config here if needed...

            base.OnModelCreating(modelBuilder);
        }
    }
}
