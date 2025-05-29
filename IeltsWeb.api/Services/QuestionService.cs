using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IeltsWeb.api.Services;

public class QuestionService : IQuestionService
{
    private readonly MyDbContext _context;
    public QuestionService(MyDbContext context) { _context = context; }

    public async Task<IEnumerable<Question>> GetAllAsync() => await _context.Questions.ToListAsync();
    public async Task<Question?> GetByIdAsync(int id) => await _context.Questions.FindAsync(id);
    public async Task<IEnumerable<Question>> GetByTestIdAsync(int testId)
    {
        return await _context.Questions.Where(q => q.TestId == testId).ToListAsync();
    }
    public async Task<Question> CreateAsync(Question question)
    {
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
        return question;
    }
    public async Task UpdateAsync(Question question)
    {
        _context.Entry(question).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var question = await _context.Questions.FindAsync(id);
        if (question != null)
        {
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }
    }
}