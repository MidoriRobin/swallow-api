using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swallow;
using Swallow.Authorization;
using Swallow.Models;
using Swallow.Services;

// Loading env files
var root = Directory.GetCurrentDirectory();

var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

// -----

var key = Environment.GetEnvironmentVariable("JWT__SECRET");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//* Load data in app settings into database settings object
builder.Services.Configure<SwallowDatabaseSettings>(
    builder.Configuration.GetSection("SwallowDatabase"));

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


// Adding authentication 
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key))
    };
});

var MyAllowSpecificOrigin = "_myAllowSpecificOrigins";

builder.Services.AddCors( x => x.AddPolicy(MyAllowSpecificOrigin, policy => {
    // TODO: Save list of allowed origins to a variable and load in one by one
    policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
}));

builder.Services.AddDbContext<SwallowContext> ( optionsAction =>

    optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//?
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<TokenBlacklistService>();
builder.Services.AddSingleton<ProjectService>();
builder.Services.AddSingleton<IssueService>();

//Injecting the user service into auth
builder.Services.AddSingleton<IJwtAuth>(serviceProvider => new Auth(key, serviceProvider.GetRequiredService<UserService>()));



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//TODO: Look into swagger stuff


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigin);

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
