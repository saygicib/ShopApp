using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasKey(x => new { x.CategoryId, x.ProductId });
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
