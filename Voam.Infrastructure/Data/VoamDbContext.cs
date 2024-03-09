using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Voam.Infrastructure.Data.Models;

namespace Voam.Infrastructure.Data
{
    public class VoamDbContext : IdentityDbContext
    {
        public VoamDbContext(DbContextOptions<VoamDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Assuming your entities are named CartItem and Size, and the foreign key property on CartItem is SizeId
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Size) // Specifies that CartItem has a single Size
                .WithMany() // Specifies that Size can be associated with many CartItems. Adjust this according to your actual navigation property, if available.
                .HasForeignKey(ci => ci.SizeId) // Specifies the foreign key property
                .OnDelete(DeleteBehavior.NoAction); // Specifies that no action should be taken on delete.

            // Optionally, you can also specify behavior for updates, though EF Core doesn't directly support 'NO ACTION' for updates in the same way as for deletes. 
            // Update behaviors are usually controlled at the application level or through database triggers if needed.

            // Add other Fluent API configurations as needed
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Review> ProductReviews { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
    }
}
