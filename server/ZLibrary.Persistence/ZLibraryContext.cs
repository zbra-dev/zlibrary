using Microsoft.EntityFrameworkCore;
using ZLibrary.Model;

namespace ZLibrary.Persistence
{
    public class ZLibraryContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ZLibraryContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
