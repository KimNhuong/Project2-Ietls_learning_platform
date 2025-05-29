using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IeltsWeb.api.Services;

public class DeepSeekRequestService : IDeepSeekRequestService
{
    private readonly MyDbContext _context;
    private readonly DeepSeekService _deepSeekService;

    public DeepSeekRequestService(MyDbContext context, DeepSeekService deepSeekService)
    {
        _context = context;
        _deepSeekService = deepSeekService;
    }

    public async Task<IEnumerable<DeepSeekRequest>> GetAllAsync() => await _context.DeepSeekRequests.ToListAsync();
    public async Task<DeepSeekRequest?> GetByIdAsync(int id) => await _context.DeepSeekRequests.FindAsync(id);
    public async Task<DeepSeekRequest> CreateAsync(DeepSeekRequest request)
    {
        _context.DeepSeekRequests.Add(request);
        await _context.SaveChangesAsync();
        return request;
    }
    public async Task UpdateAsync(DeepSeekRequest request)
    {
        _context.Entry(request).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var request = await _context.DeepSeekRequests.FindAsync(id);
        if (request != null)
        {
            _context.DeepSeekRequests.Remove(request);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<string> SendPromptAsync(string prompt)
    {
        return await _deepSeekService.CallDeepSeekAsync(prompt);
    }
}