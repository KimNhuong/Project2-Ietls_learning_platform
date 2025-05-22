// Program.cs
using IeltsWeb.api.models;
using IeltsWeb.api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IeltsWeb.api.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IExamService, ExamService>();
// Đăng ký service tiến trình
builder.Services.AddScoped<IProgressTrackingService, ProgressTrackingService>();
// Nếu có QuestionServiced<IProgressTrackingService, ProgressTrackingService>();
// builder.Services.AddScoped<IQuestionService, QuestionService>();
// builder.Services.AddScoped<IQuestionService, QuestionService>();
// Thêm authentication (demo, dùng JWT hoặc cookie nếu cần)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "IeltsWeb",
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? "IeltsWeb",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "supersecretkey"))
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen();

// Thêm cấu hình CORS cho phép frontend truy cập API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:8080")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.TTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend"); // Đảm bảo dòng này nằm TRƯỚC UseAuthentication và UseAuthorization
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<IeltsWeb.api.Middlewares.ProgressTrackingMiddleware>();
app.MapControllers();

app.Run();