using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Models.Entities;

namespace ProductManagementAPI.Models.Context
{
    public class ProductDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }

    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog=ProductDB;Integrated Security=true");
                optionsBuilder.UseSqlServer("Data Source = localhost;Initial Catalog=ProductDB;Integrated Security=true;Encrypt=False");
            }
        }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .Property(p => p.Id)
            .ValueGeneratedNever(); // Because we generate the ID manually
    }
    }
}