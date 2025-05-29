using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IeltsWeb.api.Services;

public class AnswerService : IAnswerService
{
    private readonly MyDbContext _context;
    public AnswerService(MyDbContext context) { _context = context; }

    public async Task<IEnumerable<Answer>> GetAllAsync() => await _context.Answers.ToListAsync();
    public async Task<Answer?> GetByIdAsync(int id) => await _context.Answers.FindAsync(id);
    public async Task<Answer> CreateAsync(Answer answer)
    {
        _context.Answers.Add(answer);
        await _context.SaveChangesAsync();
        return answer;
    }
    public async Task UpdateAsync(Answer answer)
    {
        _context.Entry(answer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var answer = await _context.Answers.FindAsync(id);
        if (answer != null)
        {
            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
        }
    }
} 