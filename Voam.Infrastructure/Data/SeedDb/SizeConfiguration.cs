using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voam.Infrastructure.Data.Models;

namespace Voam.Infrastructure.Data.SeedDb
{
    internal class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            var data = new SeedData();

            builder.HasData(new Size[] { data.BlackSheepHoodieSizeS, data.BlackSheepHoodieSizeM, data.BlackSheepHoodieSizeL,
            data.DevotionBeanieSizeM,
            data.DevotionTShirtSizeS, data.DevotionTShirtSizeM, data.DevotionTShirtSizeL,
            data.MidnightGreenHoodieSizeM, data.MidnightGreenHoodieSizeL,
            data.MidnightGreenPantsSizeM, data.MidnightGreenPantsSizeL,
            data.VoamHoodieSizeM});
        }
    }
}