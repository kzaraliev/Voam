﻿using Microsoft.EntityFrameworkCore;
using Voam.Core.Contracts;
using Voam.Core.Models.ShoppingCart;
using Voam.Infrastructure.Data.Models;
using Voam.Infrastucture.Data.Common;

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
                .FirstOrDefault();

            if (shoppingCart == null)
            {
                return false;
            }

            var cartItem = await cartItemService.CreateCartItem(productId, shoppingCart.Id, sizeId, quantity);
            var product = await productService.GetProductByIdAsync(productId);

            if (product == null)
            {
                return false;
            }

            var size = await sizeService.GetSizeById(sizeId);

            if (size == null)
            {
                return false;
            }

            if (size.Quantity < quantity)
            {
                return false;
            }

            shoppingCart.CartItems.Add(cartItem);
            shoppingCart.TotalAmount += product.Price;
            await repository.SaveChangesAsync();            

            return true;
        }

        public async Task CreateShoppingCartAsync(string userId)
        {
            ShoppingCart shoppingCart = new ShoppingCart()
            {
                CartItems = new List<CartItem>(),
                CustomerId = userId,
                OrderDate = DateTime.MinValue,
                TotalAmount = 0,
            };

            await repository.AddAsync(shoppingCart);
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
                    TotalAmount = sc.TotalAmount,
                    OrderDate = DateTime.MinValue,

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
