using Microsoft.EntityFrameworkCore;
using Voam.Server.Data.Models;

namespace Voam.Server.Models
{
    public class VoamDbContext : DbContext
    {
        public VoamDbContext(DbContextOptions<VoamDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Size>().HasData(
                new Size()
                {
                    Id = 1,
                    ProductId = 1,
                    Quantity = 10,
                    SizeChar = 'S',
                },
                new Size()
                {
                    Id = 2,
                    ProductId = 1,
                    Quantity = 10,
                    SizeChar = 'M',
                },
                new Size()
                {
                    Id = 3,
                    ProductId = 1,
                    Quantity = 10,
                    SizeChar = 'L',
                },
                new Size()
                {
                    Id = 4,
                    ProductId = 2,
                    Quantity = 10,
                    SizeChar = 'S',
                },
                new Size()
                {
                    Id = 5,
                    ProductId = 2,
                    Quantity = 10,
                    SizeChar = 'M',
                },
                new Size()
                {
                    Id = 6,
                    ProductId = 2,
                    Quantity = 10,
                    SizeChar = 'L',
                },
                new Size()
                {
                    Id = 7,
                    ProductId = 3,
                    Quantity = 10,
                    SizeChar = 'S',
                },
                new Size()
                {
                    Id = 8,
                    ProductId = 3,
                    Quantity = 10,
                    SizeChar = 'M',
                },
                new Size()
                {
                    Id = 9,
                    ProductId = 3,
                    Quantity = 10,
                    SizeChar = 'L',
                },
                new Size()
                {
                    Id = 10,
                    ProductId = 4,
                    Quantity = 10,
                    SizeChar = 'S',
                },
                new Size()
                {
                    Id = 11,
                    ProductId = 4,
                    Quantity = 10,
                    SizeChar = 'M',
                },
                new Size()
                {
                    Id = 12,
                    ProductId = 4,
                    Quantity = 10,
                    SizeChar = 'L',
                },
                new Size()
                {
                    Id = 13,
                    ProductId = 5,
                    Quantity = 10,
                    SizeChar = 'S',
                },
                new Size()
                {
                    Id = 14,
                    ProductId = 5,
                    Quantity = 10,
                    SizeChar = 'M',
                },
                new Size()
                {
                    Id = 15,
                    ProductId = 5,
                    Quantity = 10,
                    SizeChar = 'L',
                }
                );
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Review> ProductReviews { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
    }
}
