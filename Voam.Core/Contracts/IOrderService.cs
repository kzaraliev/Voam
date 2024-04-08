using Voam.Core.Models.Order;
using Voam.Core.Services;

namespace Voam.Core.Contracts
{
    public interface IOrderService
    {
        Task<string> PlaceOrderAsync(string userId, PlaceOrderModel model);
        Task<PaginatedList<OrderHistory>> GetAllOrdersForUserAsync(string userId, int pageSize, int pageNumber);
        Task<IEnumerable<OrderHistory>> GetAllOrdersAsync();
    }
}
