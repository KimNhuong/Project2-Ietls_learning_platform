using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IeltsWeb.api.Services;

namespace IeltsWeb.api.Middlewares;

public class ProgressTrackingMiddleware
{
    private readonly RequestDelegate _next;

    public ProgressTrackingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IProgressTrackingService progressService)
    {
        // Ví dụ: chỉ track các endpoint có chứa "/questions/grade" hoặc "/skill"
        if (context.User.Identity?.IsAuthenticated == true &&
            (context.Request.Path.Value?.Contains("/questions/grade") == true ||
             context.Request.Path.Value?.Contains("/skill") == true))
        {
            var userIdStr = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdStr, out int userId))
            {
                // Ví dụ: lấy skill từ query hoặc body, ở đây hardcode "Exam" cho demo
                await progressService.TrackProgressAsync(userId, "Exam", 100); // Có thể truyền phần trăm thực tế
            }
        }

        await _next(context);
    }
}
