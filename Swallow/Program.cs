using Swallow;
using Swallow.Models;
using Swallow.Services;

// Loading env files
var root = Directory.GetCurrentDirectory();

var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

// -----

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//* Load data in app settings into database settings object
builder.Services.Configure<SwallowDatabaseSettings>(
    builder.Configuration.GetSection("SwallowDatabase"));

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//?
builder.Services.AddSingleton<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
