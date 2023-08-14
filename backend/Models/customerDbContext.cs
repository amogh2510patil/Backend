﻿using Microsoft.EntityFrameworkCore;

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

        public DbSet<customer> customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EFCore;Trusted_Connection=True;");        
        }

    }
}
