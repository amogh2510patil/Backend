using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class customerDbContext : DbContext
    {
        public customerDbContext()
        {
        }

        public customerDbContext(DbContextOptions<customerDbContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<customer> customer { get; set; }
        public DbSet<transaction> transaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EFCore;Trusted_Connection=True;");        
        }

    }
}
