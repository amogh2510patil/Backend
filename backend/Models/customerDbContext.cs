using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class customerDbContext : DbContext
    {

        public customerDbContext(DbContextOptions<customerDbContext> options) : base(options)
        {
        }

        public DbSet<customer> customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=WINDOWS-BVQNF6J;Initial Catalog=atm; User Id=bankingapp; password=0103; Integrated Security = True; TrustServerCertificate=True");        
        }

    }
}
