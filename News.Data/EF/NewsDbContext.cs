using Microsoft.EntityFrameworkCore;
using News.Data.Configurations;
using News.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.Data.EF
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new RegisterConfiguration());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Register> Registers { get; set; }
    }
}
