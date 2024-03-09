using Voam.Infrastructure.Data.Models;

namespace Voam.Core.Contracts
{
    public interface ICartItemService
    {
        Task<CartItem> CreateCartItem(int productId, int shoppingCartId, int sizeId, int quantity);
    }
}
