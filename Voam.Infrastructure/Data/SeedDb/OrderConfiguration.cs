using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voam.Infrastructure.Data.Models;

namespace Voam.Infrastructure.Data.SeedDb
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            var data = new SeedData();

            builder.HasData(new Order[] { data.OrderCustomerUser1, data.OrderCustomerUser2, data.OrderCustomerUser3, data.OrderCustomerUser4, data.OrderCustomerUser5, data.OrderCustomerUser6, data.OrderCustomerUser7 });
        }
    }
}
