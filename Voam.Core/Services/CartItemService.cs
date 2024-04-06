using Microsoft.EntityFrameworkCore;
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

        public async Task<CartItem> CreateCartItemAsync(int productId, int shoppingCartId, int sizeId, int quantity)
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

        public async Task RemoveAllCartItemsFromShoppingCartAsync(int shoopingCartId)
        {
            var cartItemsToRemove = await repository.AllReadOnly<CartItem>()
                .Where(ci => ci.ShoppingCartId == shoopingCartId)
                .ToListAsync();

            if (cartItemsToRemove.Any())
            {
                repository.RemoveRange(cartItemsToRemove);
                await repository.SaveChangesAsync();
            }
        }
    }
}
