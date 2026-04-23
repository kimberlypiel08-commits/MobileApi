using MobileApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("PostDb"));

builder.Services.AddControllers();

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();

// Map controllers
app.MapControllers();

// Required for Render deployment
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Run($"http://0.0.0.0:{port}");