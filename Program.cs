using Microsoft.EntityFrameworkCore;
using MobileApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("PostDb"));

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors("AllowAll");

// ❌ REMOVE this on Render (safe fix)
// app.UseHttpsRedirection();

app.UseStaticFiles();

// 🔥 IMPORTANT: Render port binding
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

app.MapControllers();

// Optional test route
app.MapGet("/", () => "API is running");

app.Run();