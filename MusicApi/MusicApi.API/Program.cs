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
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Missing ConnectionStrings:DefaultConnection configuration.");

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

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
