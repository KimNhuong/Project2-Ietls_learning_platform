using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IeltsWeb.api.Services;

public class UserBookmarkService : IUserBookmarkService
{
    private readonly MyDbContext _context;
    public UserBookmarkService(MyDbContext context) { _context = context; }

    public async Task<IEnumerable<UserBookmark>> GetAllAsync() => await _context.UserBookmarks.ToListAsync();
    public async Task<UserBookmark?> GetByIdAsync(int id) => await _context.UserBookmarks.FindAsync(id);
    public async Task<UserBookmark> CreateAsync(UserBookmark bookmark)
    {
        _context.UserBookmarks.Add(bookmark);
        await _context.SaveChangesAsync();
        return bookmark;
    }
    public async Task UpdateAsync(UserBookmark bookmark)
    {
        _context.Entry(bookmark).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var bookmark = await _context.UserBookmarks.FindAsync(id);
        if (bookmark != null)
        {
            _context.UserBookmarks.Remove(bookmark);
            await _context.SaveChangesAsync();
        }
    }
} 