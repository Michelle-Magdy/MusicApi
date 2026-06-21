using MusicApi.MusicApi.API.Middleware;
using MusicApi.MusicApi.Application.Interfaces;
using MusicApi.MusicApi.Application.Services;
using MusicApi.MusicApi.Infrastructure.Data;
using MusicApi.MusicApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// add controllers
builder.Services.AddControllers();

// connect to database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlServer<AppDbContext>(connectionString);

// inject repos
builder.Services.AddScoped<IUserRepository,UserRepository>();



// inject services
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"CAUGHT: {ex.GetType().Name} - {ex.Message}");
        throw;
    }
});

app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
