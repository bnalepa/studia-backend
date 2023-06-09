using ApplicationCore.Data;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repository;
using ApplicationCore.Models;
using Infrastructure.Memory.Repository;
using Infrastructure.MongoDB.Entities;
using Infrastructure.MongoDB;
using Infrastructure.Services;
using Web;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IGenericRepository<QuizItem, int>, MemoryGenericRepository<QuizItem, int>>();
builder.Services.AddSingleton<IGenericRepository<Quiz, int>, MemoryGenericRepository<Quiz, int>>();
builder.Services.AddSingleton<IGenericRepository<QuizItemUserAnswer, string>, MemoryGenericRepository<QuizItemUserAnswer, string>>();
builder.Services.AddTransient<IQuizUserService, QuizUserServiceEF>();
builder.Services.AddDbContext<QuizDbContext>();
//builder.Services.AddTransient<IQuizUserService, QuizUserService>();
builder.Services.AddSingleton<IQuizUserService, QuizUserService>();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<QuizUserServiceMongoDB>();

builder.Services.AddSwaggerGen();

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
app.Seed();
app.Run();
public partial class Program
{
}