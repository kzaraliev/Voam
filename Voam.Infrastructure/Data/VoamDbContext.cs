using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Voam.Infrastructure.Data.Models;

namespace Voam.Infrastructure.Data
{
    public class VoamDbContext : IdentityDbContext
    {
        public VoamDbContext(DbContextOptions<VoamDbContext> options) : base(options)
        { }

       

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Review> ProductReviews { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
    }
}
