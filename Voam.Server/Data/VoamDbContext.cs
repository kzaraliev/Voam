using Microsoft.EntityFrameworkCore;
using Voam.Server.Data.Models;
using Voam.Server.Utils;

namespace Voam.Server.Models
{
    public class VoamDbContext : DbContext
    {
        public VoamDbContext(DbContextOptions<VoamDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Hoodie 1",
                    Description = "A comfortable cotton hoodie.",
                    Price = 19.99m,
                    IsAvailable = true,
                    Image = ImageConverter.GetImageDataFromFile("./hoodie1.png"),
                },
                new Product
                {
                    Id = 2,
                    Name = "Hoodie 2",
                    Description = "A comfortable hoodie",
                    Price = 19.99m,
                    IsAvailable = true,
                    Image = ImageConverter.GetImageDataFromFile("./hoodie2.png")
                },
                new Product
                {
                    Id = 3,
                    Name = "Cap",
                    Description = "A comfortable cotton Cap.",
                    Price = 7.99m,
                    IsAvailable = true,
                    Image = ImageConverter.GetImageDataFromFile("./cap.png")
                },
                new Product
                {
                    Id = 4,
                    Name = "Hoodie 3",
                    Description = "A comfortable cotton hoodie.",
                    Price = 9.99m,
                    IsAvailable = true,
                    Image = ImageConverter.GetImageDataFromFile("./hoodie3.png")
                },
                new Product
                {
                    Id = 5,
                    Name = "Hoodie 4",
                    Description = "A comfortable cotton hoodie number 4.",
                    Price = 99.99m,
                    IsAvailable = true,
                    Image = ImageConverter.GetImageDataFromFile("./hoodie4.png")
                }

            );
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
    }
}
