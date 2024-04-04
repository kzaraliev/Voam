using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voam.Infrastructure.Data.Models;

namespace Voam.Infrastructure.Data.SeedDb
{
    internal class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            var data = new SeedData();

            builder.HasData(new ShoppingCart[] { data.ShoppingCartAdmin, data.ShoppingCartCustomer});
        }
    }
}
