using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IeltsWeb.api.Services;

public class MediaService : IMediaService
{
    private readonly MyDbContext _context;
    public MediaService(MyDbContext context) { _context = context; }

    public async Task<IEnumerable<Media>> GetAllAsync() => await _context.Media.ToListAsync();
    public async Task<Media?> GetByIdAsync(int id) => await _context.Media.FindAsync(id);
    public async Task<Media> CreateAsync(Media media)
    {
        _context.Media.Add(media);
        await _context.SaveChangesAsync();
        return media;
    }
    public async Task UpdateAsync(Media media)
    {
        _context.Entry(media).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var media = await _context.Media.FindAsync(id);
        if (media != null)
        {
            _context.Media.Remove(media);
            await _context.SaveChangesAsync();
        }
    }
} 