using Voam.Core.Models.ShoppingCart;
using Voam.Infrastructure.Data.Models;
using static Voam.Core.Utils.Constants;

namespace Voam.Core.Contracts
{
    public interface IShoppingCartService
    {
        Task<DisplayShoppingCartModel?> GetShoppingCartAsync(string userId);
        Task CreateShoppingCartAsync(string userId);
        Task<bool> AddCartItemAsync(string userId, int productId, int sizeId, int quantity);
        Task EmptyShoppingCartAsync(string userId);
        Task<DeleteResult> DeleteCartItemAsyncByIdAsync(int cartItemId);
    }
}
