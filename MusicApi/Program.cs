using MusicApi.MusicApi.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlServer<AppDbContext>(connectionString);




var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
