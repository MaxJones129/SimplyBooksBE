using Microsoft.EntityFrameworkCore;
using SimplyBooks.Models;

public class SimplyBooksDbContext : DbContext
{

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    public SimplyBooksDbContext(DbContextOptions<SimplyBooksDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    // seed data with campsite types
    modelBuilder.Entity<Author>().HasData(new Author[]
    {
        new Author { Id = 1, FirstName = "Charlotte", LastName = "Perkins Gilman", Email = "charlotte.perkins@example.com", Image = "charlotte.jpg", Favorite = true, Uid = "UID001" },
        new Author { Id = 2, FirstName = "Langston", LastName = "Hughes", Email = "langston.hughes@example.com", Image = "langston.jpg", Favorite = false, Uid = "UID002" },
        new Author { Id = 3, FirstName = "Zora", LastName = "Neale Hurston", Email = "zora.hurston@example.com", Image = "zora.jpg", Favorite = true, Uid = "UID003" },
        new Author { Id = 4, FirstName = "James", LastName = "Baldwin", Email = "james.baldwin@example.com", Image = "james.jpg", Favorite = false, Uid = "UID004" },
        new Author { Id = 5, FirstName = "Toni", LastName = "Morrison", Email = "toni.morrison@example.com", Image = "toni.jpg", Favorite = true, Uid = "UID005" },
    });

    modelBuilder.Entity<Book>().HasData(new Book[]
    {
        new Book { Id = 1, Title = "Women and Economics", Description = "A pioneering feminist work by Charlotte Perkins Gilman examining the economic independence of women.", AuthorId = 1, Image = "women_economics.jpg", Price = 13.50m, Sale = false, Uid = "BID001" },
        new Book { Id = 2, Title = "The Breaking Point", Description = "A short story collection by Daphne Du Maurier exploring psychological tension and mystery.", AuthorId = 4, Image = "breaking_point.jpg", Price = 9.25m, Sale = true, Uid = "BID002" },
        new Book { Id = 3, Title = "The Mistress of Silence", Description = "A reflective novel by Jacqueline Harpman about a woman's inner world and the meaning of solitude.", AuthorId = 3, Image = "mistress_silence.jpg", Price = 10.99m, Sale = false, Uid = "BID003" },
        new Book { Id = 4, Title = "Ghost Music", Description = "A haunting tale by Ling Ling Huang about a concert pianist whose emotional unraveling blurs the lines between art and grief.", AuthorId = 2, Image = "ghost_music.jpg", Price = 13.75m, Sale = true, Uid = "BID004" },
        new Book { Id = 5, Title = "How Long 'Til Black Future Month?", Description = "A collection of short stories by N.K. Jemisin blending speculative fiction with social commentary.", AuthorId = 5, Image = "black_future_month.jpg", Price = 11.50m, Sale = false, Uid = "BID005" },
    });
  }
}
