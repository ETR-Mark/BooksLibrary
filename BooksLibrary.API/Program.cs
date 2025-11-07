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
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.WriteIndented = true;
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();

var categoryGroup = app.MapGroup("/api/Categories");
var bookGroup = app.MapGroup("/api/Books");

categoryGroup.MapGet("/", async (ICategoryRepository categoryRepository) =>
{
    var categories = await categoryRepository.GetAllAsync();
    return categories;
});

categoryGroup.MapGet("/{id}", async (int id, ICategoryRepository categoryRepository) =>
{
    var category = await categoryRepository.GetByIdAsync(id);
    return category;
});

categoryGroup.MapPost("/create", async (CreateCategoryDTO category, ICategoryService categoryService) =>
{
    return await categoryService.CreateCategoryAsync(category);
});

categoryGroup.MapPut("/{id}/update", async (int id, CreateCategoryDTO category, ICategoryService categoryService) =>
{
    return await categoryService.UpdateCategoryAsync(id, category);
});

categoryGroup.MapDelete("/{id}/delete", async (int id, ICategoryService categoryService) =>
{
    await categoryService.DeleteCategoryAsync(id);
    return $"Category {id} deleted";
});

categoryGroup.MapPost("/{id}/books", async (int id, CreateBookDTO book, ICategoryService categoryService) =>
{
    return await categoryService.AddBookAsync(id, book);
});

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
