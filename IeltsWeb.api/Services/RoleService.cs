using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IeltsWeb.api.Services;

public class RoleService : IRoleService
{
    private readonly MyDbContext _context;
    public RoleService(MyDbContext context) { _context = context; }

    public async Task<IEnumerable<Role>> GetAllAsync() => await _context.Roles.ToListAsync();
    public async Task<Role?> GetByIdAsync(int id) => await _context.Roles.FindAsync(id);
    public async Task<Role> CreateAsync(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }
    public async Task UpdateAsync(Role role)
    {
        _context.Entry(role).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role != null)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }
    }
} 