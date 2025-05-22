using IeltsWeb.api.models;
using Microsoft.EntityFrameworkCore;

namespace IeltsWeb.api.Services;

public class ProgressTrackingService : IProgressTrackingService
{
    private readonly MyDbContext _context;

    public ProgressTrackingService(MyDbContext context)
    {
        _context = context;
    }

    public async Task TrackProgressAsync(int userId, string skill, double percent)
    {
        var progress = await _context.ProgressTrackings
            .FirstOrDefaultAsync(p => p.UserId == userId && p.Skill == skill);

        if (progress == null)
        {
            progress = new ProgressTracking
            {
                UserId = userId,
                Skill = skill,
                ProcessPercentage = percent,
                LastUpdated = DateTime.UtcNow
            };
            _context.ProgressTrackings.Add(progress);
        }
        else
        {
            progress.ProcessPercentage = percent;
            progress.LastUpdated = DateTime.UtcNow;
        }
        await _context.SaveChangesAsync();
    }
}
