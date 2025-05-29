using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IeltsWeb.api.Services;

public class QuestionMediaService : IQuestionMediaService
{
    private readonly MyDbContext _context;
    public QuestionMediaService(MyDbContext context) { _context = context; }

    public async Task<IEnumerable<QuestionMedia>> GetAllAsync() => await _context.QuestionMedias.ToListAsync();
    public async Task<QuestionMedia?> GetByIdAsync(int questionId, int mediaId) =>
        await _context.QuestionMedias.FindAsync(questionId, mediaId);
    public async Task<QuestionMedia> CreateAsync(QuestionMedia entity)
    {
        _context.QuestionMedias.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task DeleteAsync(int questionId, int mediaId)
    {
        var entity = await _context.QuestionMedias.FindAsync(questionId, mediaId);
        if (entity != null)
        {
            _context.QuestionMedias.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
} 