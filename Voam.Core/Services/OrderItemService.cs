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

        public async Task<IEnumerable<OrderItem>> CreateOrderItemsAsync(int orderId, List<OrderItemCreateModel> products)
        {
            List<OrderItem> items = new List<OrderItem>();

            foreach (var product in products)
            {
                OrderItem orderItem = new OrderItem()
                {
                    Quantity = product.Quantity,
                    Name = product.Name,
                    Price = product.Price,
                    OrderId = orderId,
                    SizeChar = product.SizeChar,
                };

                items.Add(orderItem);
            }


            await repository.AddRangeAsync(items);
            await repository.SaveChangesAsync();

            return items;
        }
    }
}
