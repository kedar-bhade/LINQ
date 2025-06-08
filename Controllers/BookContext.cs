using Microsoft.EntityFrameworkCore;

namespace LINQ
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
    }
}
