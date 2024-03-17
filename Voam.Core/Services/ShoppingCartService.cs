using Microsoft.EntityFrameworkCore;
using Voam.Core.Contracts;
using Voam.Core.Models.ShoppingCart;
using Voam.Infrastructure.Data.Models;
using Voam.Infrastucture.Data.Common;
using static Voam.Core.Utils.Constants;

namespace Voam.Core.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository repository;
        private readonly ICartItemService cartItemService;
        private readonly IProductService productService;
        private readonly ISizeService sizeService;

        public ShoppingCartService(IRepository _repository, ICartItemService _cartItemService, IProductService _productService, ISizeService _sizeService)
        {
            repository = _repository;
            cartItemService = _cartItemService;
            productService = _productService;
            sizeService = _sizeService;
        }

        public async Task<bool> AddCartItemAsync(string userId, int productId, int sizeId, int quantity)
        {
            var shoppingCart = repository.All<ShoppingCart>()
                .Where(sc => sc.CustomerId == userId)
                .Select(sc => new ShoppingCart()
                {
                    CartItems = sc.CartItems,
                    CustomerId = sc.CustomerId,
                    Id = sc.Id
                })
                .FirstOrDefault();

            if (shoppingCart == null)
            {
                return false;
            }

            var product = await productService.GetProductByIdAsync(productId);

            if (product == null)
            {
                return false;
            }

            var size = await sizeService.GetSizeByIdAsync(sizeId);

            if (size == null)
            {
                return false;
            }

            if (size.Quantity < quantity)
            {
                return false;
            }

            if (shoppingCart.CartItems.Any(ci => ci.ProductId == productId) && shoppingCart.CartItems.Any(ci => ci.SizeId == sizeId))
            {
                var existingCartItem = shoppingCart.CartItems.Where(ci => ci.ProductId == productId).FirstOrDefault();

                if (existingCartItem == null)
                {
                    return false;
                }

                if (size.Quantity < quantity + existingCartItem.Quantity)
                {
                    return false;
                }

                existingCartItem.Quantity += quantity;

                await repository.SaveChangesAsync();
                return true;
            }

            var cartItem = await cartItemService.CreateCartItemAsync(productId, shoppingCart.Id, sizeId, quantity);

            shoppingCart.CartItems.Add(cartItem);
            await repository.SaveChangesAsync();            

            return true;
        }

        public async Task CreateShoppingCartAsync(string userId)
        {
            ShoppingCart shoppingCart = new ShoppingCart()
            {
                CartItems = new List<CartItem>(),
                CustomerId = userId,
            };

            await repository.AddAsync(shoppingCart);
            await repository.SaveChangesAsync();
        }

        public async Task<DeleteResult> DeleteCartItemAsyncByIdAsync(int cartItemId)
        {
            var cartItem = await repository.FindAsync<CartItem>(cartItemId);

            if (cartItem == null)
            {
                return DeleteResult.NotFound;
            }

            var shoppingCart = await repository.FindAsync<ShoppingCart>(cartItem.ShoppingCartId);
            
            if (shoppingCart == null)
            {
                return DeleteResult.NotFound;
            }

            var product = await repository.FindAsync<Product>(cartItem.ProductId);

            if (product == null)
            {
                return DeleteResult.NotFound;
            }

            repository.Remove(cartItem);
            int saveResult = await repository.SaveChangesAsync();

            return saveResult > 0 ? DeleteResult.Success : DeleteResult.Error;
        }

        public async Task EmptyShoppingCartAsync(string userId)
        {
            var shoppingCart =  await repository.All<ShoppingCart>()
                .Where(sc => sc.CustomerId == userId)
                .FirstOrDefaultAsync();

            if (shoppingCart == null)
            {
                throw new InvalidOperationException("There is no such shopping cart");
            }

            await cartItemService.RemoveAllCartItemsFromShoppingCartAsync(shoppingCart.Id);

            await repository.SaveChangesAsync();
        }

        public async Task<DisplayShoppingCartModel?> GetShoppingCartAsync(string userId)
        {
            try
            {
                return await repository.AllReadOnly<ShoppingCart>()
                .Where(sc => sc.CustomerId == userId)
                .Select(sc => new DisplayShoppingCartModel()
                {
                    Id = sc.Id,
                    CustomerId = sc.CustomerId,
                    CartItems = sc.CartItems.Select(ci => new CartItemsModel()
                    {
                        Id = ci.Id,
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity,
                        SizeId = ci.SizeId,
                    }).ToList(),
                    TotalPrice = sc.CartItems.Sum(sc => sc.Product.Price * sc.Quantity)
                })
                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("A problem occurred while fetching details for shopping cart", ex);
            }
        }
    }
}
