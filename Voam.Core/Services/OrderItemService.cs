using Microsoft.AspNetCore.Identity;
using Voam.Core.Contracts;
using Voam.Core.Models.Order;
using Voam.Infrastructure.Data.Models;
using Voam.Infrastucture.Data.Common;

namespace Voam.Core.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository repository;

        public OrderItemService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<OrderItem>> CreateOrderItemsAsync(int orderId, IEnumerable<CartItem> products)
        {
            List<OrderItem> items = new List<OrderItem>();

            foreach (var product in products)
            {
                OrderItem orderItem = new OrderItem()
                {
                    Quantity = product.Quantity,
                    ProductId = product.ProductId,
                    OrderId = orderId,
                    SizeId = product.SizeId,
                };

                items.Add(orderItem);
            }


            await repository.AddRangeAsync(items);
            await repository.SaveChangesAsync();

            return items;
        }
    }
}
