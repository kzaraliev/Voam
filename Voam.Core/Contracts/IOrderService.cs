using Voam.Core.Models.Order;

namespace Voam.Core.Contracts
{
    public interface IOrderService
    {
        Task<string> PlaceOrderAsync(string userId, PlaceOrderModel model);
        Task<IEnumerable<OrderHistory>> GetAllOrdersForUserAsync(string userId);
        Task<IEnumerable<OrderHistory>> GetAllOrdersAsync();
    }
}
