using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IeltsWeb.api.Services;

public class TestAttemptService : ITestAttemptService
{
    private readonly MyDbContext _context;
    public TestAttemptService(MyDbContext context) { _context = context; }

    public async Task<IEnumerable<TestAttempt>> GetAllAsync() => await _context.TestAttempts.ToListAsync();
    public async Task<TestAttempt?> GetByIdAsync(int id) => await _context.TestAttempts.FindAsync(id);
    public async Task<TestAttempt> CreateAsync(TestAttempt attempt)
    {
        _context.TestAttempts.Add(attempt);
        await _context.SaveChangesAsync();
        return attempt;
    }
    public async Task UpdateAsync(TestAttempt attempt)
    {
        _context.Entry(attempt).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var attempt = await _context.TestAttempts.FindAsync(id);
        if (attempt != null)
        {
            _context.TestAttempts.Remove(attempt);
            await _context.SaveChangesAsync();
        }
    }
} 