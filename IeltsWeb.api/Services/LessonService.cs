using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IeltsWeb.api.Services;

public class LessonService : ILessonService
{
    private readonly MyDbContext _context;
    public LessonService(MyDbContext context) { _context = context; }

    public async Task<IEnumerable<Lesson>> GetAllAsync() => await _context.Lessons.ToListAsync();
    public async Task<Lesson?> GetByIdAsync(int id) => await _context.Lessons.FindAsync(id);
    public async Task<Lesson> CreateAsync(Lesson lesson)
    {
        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();
        return lesson;
    }
    public async Task UpdateAsync(Lesson lesson)
    {
        _context.Entry(lesson).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson != null)
        {
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<Lesson>> GetByCourseIdAsync(int courseId)
    {
        return await _context.Lessons.Where(l => l.CourseId == courseId).ToListAsync();
    }
} 