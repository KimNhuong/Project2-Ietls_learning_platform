using IeltsWeb.api.Models;
using Microsoft.EntityFrameworkCore;

namespace IeltsWeb.api.models;

public class MyDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<ExamResult> ExamResults { get; set; }
    public DbSet<SkillExercise> SkillExercises { get; set; }
    public DbSet<SkillResult> SkillResults { get; set; }
    public DbSet<ProgressTracking> ProgressTrackings { get; set; }
    public DbSet<DeepSeekRequest> DeepSeekRequests { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>()
    .HasOne(u => u.Role) // Chỉ định User có một Role
    .WithMany(r => r.Users) // Role có nhiều Users
    .HasForeignKey(u => u.RoleId); // Chỉ định RoleId là khóa ngoại

    
    // Seed data cho bảng Roles
    modelBuilder.Entity<Role>().HasData(
        new Role { RoleId = 1, RoleName = "Admin" },
        new Role { RoleId = 2, RoleName = "User" }
    );
}
}