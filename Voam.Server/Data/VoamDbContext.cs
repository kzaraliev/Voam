using Microsoft.EntityFrameworkCore;
using Voam.Server.Data.Models;

namespace Voam.Server.Models
{
    public class VoamDbContext : DbContext
    {
        public VoamDbContext(DbContextOptions<VoamDbContext> options) : base(options)
        { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>().HasData();
        //
        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Review> ProductReviews { get; set; }
    }
}
