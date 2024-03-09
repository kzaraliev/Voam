using Voam.Core.Contracts;
using Voam.Infrastructure.Data.Models;
using Voam.Infrastucture.Data.Common;

namespace Voam.Core.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly IRepository repository;

        public CartItemService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<CartItem> CreateCartItem(int productId, int shoppingCartId, int sizeId, int quantity)
        {
            CartItem cartItem = new CartItem()
            {
                Quantity = quantity,
                ProductId = productId,
                ShoppingCartId = shoppingCartId,
                SizeId = sizeId,
            };

            await repository.AddAsync(cartItem);
            await repository.SaveChangesAsync();

            return cartItem;
        }
    }
}
