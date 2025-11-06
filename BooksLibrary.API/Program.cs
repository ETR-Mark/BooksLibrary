using BooksLibrary.Application.DTOs;
using BooksLibrary.Application.Mappings;
using BooksLibrary.Application.Services;
using BooksLibrary.Domain.Interfaces.Repositories;
using BooksLibrary.Domain.Interfaces.Services;
using BooksLibrary.Infrastructure.Persistence;
using BooksLibrary.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddDbContext<BooksLibraryDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

var bookGroup = app.MapGroup("/api/Books");

bookGroup.MapGet("/", async (IBooksService booksService) =>
{
    return await booksService.GetBooksAsync();
});

bookGroup.MapGet("/{id}", async (int id, IBooksService booksService) =>
{
    return await booksService.GetBookByIdAsync(id);
});

bookGroup.MapPost("/create", async (CreateBookDTO book, IBooksService booksService) =>
{
    return await booksService.AddAsync(book);
});

bookGroup.MapPut("/{id}/update", async (int id, CreateBookDTO book, IBooksService booksService) =>
{
    return await booksService.UpdateBook(id, book);
});

bookGroup.MapDelete("/{id}/delete", async (int id,IBooksService booksService) =>
{
    await booksService.DeleteBookAsync(id);
    return $"Book {id} deleted";
});

bookGroup.MapPost("/{id}/reviews", async (int id, CreateReviewDTO review, IBooksService booksService) =>
{
    return await booksService.AddReviewAsync(id, review);
});

app.Run();
