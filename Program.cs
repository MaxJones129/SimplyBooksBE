using SimplyBooks.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<SimplyBooksDbContext>(builder.Configuration["SimplyBooksDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/authors", (SimplyBooksDbContext db) =>
{
    return db.Authors.ToList();
});

app.MapGet("/books", (SimplyBooksDbContext db) =>
{
    return db.Books.ToList();
});

app.MapGet("/authors/favorite", (SimplyBooksDbContext db) =>
{
    var favoriteAuthors = db.Authors.Where(a => a.Favorite == true).ToList(); // Filter favorite authors synchronously
    return favoriteAuthors;
});

app.MapPost("/authors", (SimplyBooksDbContext db, Author author) =>
{
    db.Authors.Add(author);
    db.SaveChanges();  // Save changes synchronously
    return Results.Created($"/authors/{author.Id}", author);
});

app.MapPost("/books", (SimplyBooksDbContext db, Book book) =>
{
    db.Books.Add(book);
    db.SaveChanges();  // Save changes synchronously
    return Results.Created($"/books/{book.Id}", book);
});

app.MapPut("/authors/{id}", (SimplyBooksDbContext db, int id, Author updatedAuthor) =>
{
    var author = db.Authors.FirstOrDefault(a => a.Id == id);
    if (author is null)
    {
        return Results.NotFound();
    }
    
    author.FirstName = updatedAuthor.FirstName;
    author.LastName = updatedAuthor.LastName;
    author.Email = updatedAuthor.Email;
    author.Image = updatedAuthor.Image;
    author.Favorite = updatedAuthor.Favorite;
    author.Uid = updatedAuthor.Uid;

    db.SaveChanges();  // Save changes synchronously
    return Results.Ok(author);
});

app.MapPut("/books/{id}", (SimplyBooksDbContext db, int id, Book updatedBook) =>
{
    var book = db.Books.FirstOrDefault(b => b.Id == id);
    if (book is null)
    {
        return Results.NotFound();
    }

    book.Title = updatedBook.Title;
    book.Description = updatedBook.Description;
    book.AuthorId = updatedBook.AuthorId;
    book.Image = updatedBook.Image;
    book.Price = updatedBook.Price;
    book.Sale = updatedBook.Sale;
    book.Uid = updatedBook.Uid;

    db.SaveChanges();  // Save changes synchronously
    return Results.Ok(book);
});

app.MapDelete("/authors/{id}", (SimplyBooksDbContext db, int id) =>
{
    var author = db.Authors.FirstOrDefault(a => a.Id == id);
    if (author is null)
    {
        return Results.NotFound();
    }

    db.Authors.Remove(author);
    db.SaveChanges();  // Save changes synchronously
    return Results.NoContent();
});

app.MapDelete("/books/{id}", (SimplyBooksDbContext db, int id) =>
{
    var book = db.Books.FirstOrDefault(b => b.Id == id);
    if (book is null)
    {
        return Results.NotFound();
    }

    db.Books.Remove(book);
    db.SaveChanges();  // Save changes synchronously
    return Results.NoContent();
});

app.MapGet("/authors/{authorId}/books", (SimplyBooksDbContext db, int authorId) =>
{
    var books = db.Books.Where(b => b.AuthorId == authorId).ToList();
    if (books == null || books.Count == 0)
    {
        return Results.NotFound("No books found for the given author.");
    }
    return Results.Ok(books);
});


app.Run();
