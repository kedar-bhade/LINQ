using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using LINQ.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAuthorRepository, AuthorRepository>();
builder.Services.AddSingleton<IBookRepository, BookRepository>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
