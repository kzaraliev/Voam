using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voam.Infrastructure.Data.Models;

namespace Voam.Infrastructure.Data.SeedDb
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            var data = new SeedData();

            builder.HasData(new Product[] { data.BlackSheepHoodie, data.DevotionBeanie, data.DevotionTShirt, data.MidnightGreenHoodie, data.MidnightGreenPants, data.VoamHoodie });
        }
    }
}