using Microsoft.EntityFrameworkCore;
using ZLibrary.Model;

namespace ZLibrary.Persistence
{
    public class ZLibraryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Isbn> Isbns { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public ZLibraryContext(DbContextOptions options)
            : base(options)
        {
        }

    }
}