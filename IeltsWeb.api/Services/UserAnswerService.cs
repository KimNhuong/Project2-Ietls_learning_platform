using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IeltsWeb.api.Services;

public class UserAnswerService : IUserAnswerService
{
    private readonly MyDbContext _context;
    public UserAnswerService(MyDbContext context) { _context = context; }

    public async Task<IEnumerable<UserAnswer>> GetAllAsync() => await _context.UserAnswers.ToListAsync();
    public async Task<UserAnswer?> GetByIdAsync(int id) => await _context.UserAnswers.FindAsync(id);
    public async Task<UserAnswer> CreateAsync(UserAnswer answer)
    {
        _context.UserAnswers.Add(answer);
        await _context.SaveChangesAsync();
        return answer;
    }
    public async Task UpdateAsync(UserAnswer answer)
    {
        _context.Entry(answer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var answer = await _context.UserAnswers.FindAsync(id);
        if (answer != null)
        {
            _context.UserAnswers.Remove(answer);
            await _context.SaveChangesAsync();
        }
    }
} 