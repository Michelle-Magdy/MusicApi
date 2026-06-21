using Microsoft.EntityFrameworkCore;
using MusicApi.MusicApi.API.Middleware;
using MusicApi.MusicApi.Application.Interfaces;
using MusicApi.MusicApi.Application.Services;
using MusicApi.MusicApi.Domain.Interfaces;
using MusicApi.MusicApi.Infrastructure.Data;
using MusicApi.MusicApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// add controllers
builder.Services.AddControllers();

// connect to database
// Prefer explicit environment variables for sensitive values (DB_SERVER, DB_USER, DB_PASSWORD).
// Falls back to the DefaultConnection from configuration if env vars are not present.

var dbHost = Environment.GetEnvironmentVariable("DB_HOST"); // e.g. "localhost,1433" or "sqlserver"
var dbName = Environment.GetEnvironmentVariable("DB_NAME");     // e.g. "sa"
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD"); // set via env var / secrets

var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};TrustServerCertificate=True;Encrypt=False;MultipleActiveResultSets=true";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure())
);

// inject repos
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<ISongRepository,SongRepository>();
builder.Services.AddScoped<IPlaylistRepository,PlaylistRepository>();


// inject services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();


var app = builder.Build();


app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
