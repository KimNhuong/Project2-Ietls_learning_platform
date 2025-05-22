using IeltsWeb.api.models;
using IeltsWeb.api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using IeltsWeb.api.Interfaces;
namespace IeltsWeb.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly MyDbContext _context;
    private readonly IUserService _userService;
    private readonly IConfiguration _config;

    public AuthController(MyDbContext context, IUserService userService, IConfiguration config)
    {
        _context = context;
        _userService = userService;
        _config = config;
    }

    // POST: /api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest req)
    {
        if (await _context.Users.AnyAsync(u => u.Username == req.Username || u.Email == req.Email))
            return BadRequest(new { message = "Username or Email already exists" });

        var user = new User
        {
            Username = req.Username,
            Email = req.Email,
            PasswordHash = HashPassword(req.Password),
            RoleId = 2 // Default role: User
        };
        await _userService.CreateUserAsync(user);
        return Ok(new { message = "Registration successful" });
    }

    // POST: /api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var user = await _context.Users.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Username == req.Username);
        if (user == null || !VerifyPassword(req.Password, user.PasswordHash))
            return Unauthorized(new { message = "Invalid username or password" });

        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    // POST: /api/auth/forgot-password
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest req)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == req.Email);
        if (user == null)
            return Ok(new { message = "If the email exists, a reset link will be sent." });

        // TODO: Generate token, send email (demo: return token)
        var token = Guid.NewGuid().ToString();
        // Save token to DB or cache (not implemented)
        return Ok(new { message = "Reset link sent to email (demo)", token });
    }

    // POST: /api/auth/reset-password
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest req)
    {
        // TODO: Validate token (not implemented)
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == req.Email);
        if (user == null)
            return BadRequest(new { message = "Invalid request" });

        user.PasswordHash = HashPassword(req.NewPassword);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Password reset successful" });
    }

    // POST: /api/auth/change-password/{id}
    [Authorize]
    [HttpPost("change-password/{id}")]
    public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordRequest req)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null || !VerifyPassword(req.OldPassword, user.PasswordHash))
            return BadRequest(new { message = "Old password is incorrect" });

        user.PasswordHash = HashPassword(req.NewPassword);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Password changed successfully" });
    }

    // GET: /api/auth/me
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == userId);
        if (user == null) return NotFound();
        return Ok(new { user.UserId, user.Username, user.Email, Role = user.Role?.RoleName });
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.RoleId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "supersecretkey"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"] ?? "IeltsWeb",
            audience: _config["Jwt:Audience"] ?? "IeltsWeb",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(12),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // --- Helper methods and DTOs ---
    private static string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private static bool VerifyPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }

    public class RegisterRequest
    {
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class LoginRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class ForgotPasswordRequest
    {
        public string Email { get; set; } = "";
    }

    public class ResetPasswordRequest
    {
        public string Email { get; set; } = "";
        public string Token { get; set; } = "";
        public string NewPassword { get; set; } = "";
    }

    public class ChangePasswordRequest
    {
        public string OldPassword { get; set; } = "";
        public string NewPassword { get; set; } = "";
    }
}
