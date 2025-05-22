using IeltsWeb.api.models;

namespace IeltsWeb.api.Services;

public interface IProgressTrackingService
{
    Task TrackProgressAsync(int userId, string skill, double percent);
}
