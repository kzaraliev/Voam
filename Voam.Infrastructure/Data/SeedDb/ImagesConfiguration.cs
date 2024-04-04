using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voam.Infrastructure.Data.Models;

namespace Voam.Infrastructure.Data.SeedDb
{
    internal class ImagesConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            var data = new SeedData();

            builder.HasData(new ProductImage[] { data.BlackSheepHoodieImg1, data.BlackSheepHoodieImg2, data.BlackSheepHoodieImg3,
            data.DevotionBeanieImg1, data.DevotionBeanieImg2,
            data.DevotionTShirtImg1, data.DevotionTShirtImg2,
            data.MidnightGreenHoodieImg1, data.MidnightGreenHoodieImg2,
            data.MidnightGreenPantsImg1, data.MidnightGreenPantsImg2,
            data.VoamHoodieImg1, data.VoamHoodieImg2});
        }
    }
}