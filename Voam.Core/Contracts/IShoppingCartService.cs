using Voam.Core.Models.ShoppingCart;
using Voam.Infrastructure.Data.Models;

namespace Voam.Core.Contracts
{
    public interface IShoppingCartService
    {
        Task<DisplayShoppingCartModel?> GetShoppingCartAsync(string userId);
        Task CreateShoppingCartAsync(string userId);
        Task<bool> AddCartItemAsync(string userId, int productId, int sizeId, int quantity);
    }
}
