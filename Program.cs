using Microsoft.EntityFrameworkCore;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using AmadeusG3_Neo_Tech_BackEnd.Services;
using AmadeusG3_Neo_Tech_BackEnd.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<QuestionRepository>();
builder.Services.AddScoped<Question_OptionsService>();
builder.Services.AddScoped<Question_OptionsRepository>();
builder.Services.AddScoped<CityService>();
builder.Services.AddScoped<CityRepository>();
builder.Services.AddScoped<DestinationService>();
builder.Services.AddScoped<DestinationRepository>();

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

