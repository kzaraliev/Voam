using Voam.Core.Models.Order;

namespace Voam.Core.Contracts
{
    public interface IOrderService
    {
        Task<bool> PlaceOrderAsync(string userId, PlaceOrderModel model);
    }
}
