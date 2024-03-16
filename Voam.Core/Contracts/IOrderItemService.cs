using Voam.Infrastructure.Data.Models;

namespace Voam.Core.Contracts
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItem>> CreateOrderItemsAsync(int orderId, IEnumerable<CartItem> products);
    }
}
