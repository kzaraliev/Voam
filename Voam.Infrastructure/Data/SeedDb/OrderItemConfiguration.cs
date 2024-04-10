using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voam.Infrastructure.Data.Models;

namespace Voam.Infrastructure.Data.SeedDb
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            var data = new SeedData();

            builder.HasData(new OrderItem[] { data.OrderItemBlackSheepHoodie1, data.OrderItemBlackSheepHoodie2, data.OrderItemDevotionBeanie1, data.OrderItemDevotionBeanie2, data.OrderItemDevotionTShirt, data.OrderItemMidnightGreenHoodie, data.OrderItemMidnightGreenPants, data.OrderItemVoamHoodie1, data.OrderItemVoamHoodie2});
        }
    }
}
