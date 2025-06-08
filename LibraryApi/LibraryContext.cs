using Microsoft.EntityFrameworkCore;
using LibraryApi.Models;

namespace LibraryApi
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Book> Books => Set<Book>();
    }
}
