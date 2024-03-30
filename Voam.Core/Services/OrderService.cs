using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Voam.Core.Contracts;
using Voam.Core.Models.Order;
using Voam.Infrastructure.Data.Models;
using Voam.Infrastucture.Data.Common;

namespace Voam.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository repository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IOrderItemService orderItemService;
        private readonly IShoppingCartService shoppingCartService;

        public OrderService(IRepository _repository, UserManager<IdentityUser> _userManager, IOrderItemService _orderItemService, IShoppingCartService _shoppingCartService)
        {
            repository = _repository;
            userManager = _userManager;
            orderItemService = _orderItemService;
            shoppingCartService = _shoppingCartService;
        }

        public async Task<string> PlaceOrderAsync(string userId, PlaceOrderModel model)
        {
            var identityUser = await userManager.FindByIdAsync(userId);
            decimal totalPrice = 0m;
            List<OrderItemCreateModel> products = new List<OrderItemCreateModel>();

            if (identityUser == null)
            {
                throw new InvalidOperationException("No such user");
            }

            foreach (var item in model.Products)
            {
                var product = await repository.AllReadOnly<Product>()
                      .Where(p => p.Id == item.ProductId)
                      .FirstOrDefaultAsync();

                if (product == null)
                {
                    throw new InvalidOperationException($"Product with Id {item.Id} does not exist");
                }

                var size = await repository.All<Size>()
                        .Where(s => s.ProductId == product.Id)
                        .FirstOrDefaultAsync();

                if (size == null)
                {
                    throw new InvalidOperationException("No size found");
                }

                if (size.Quantity < item.Quantity)
                {
                    return "Not enough units";
                }

                size.Quantity -= item.Quantity;
                products.Add(new OrderItemCreateModel()
                { 
                    Name = product.Name,
                    Quantity = size.Quantity,
                    Price = product.Price,
                    SizeChar = size.SizeChar,
                });
                totalPrice += product.Price * item.Quantity;
            }

            Order order = new Order()
            {
                City = model.City,
                CustomerId = identityUser.Id,
                Econt = model.EcontOffice,
                FullName = model.FullName,
                Email = model.Email,
                OrderDate = DateTime.Now,
                PaymentMethod = model.PaymentMethod,
                PhoneNumber = model.PhoneNumber,
                TotalPrice = totalPrice,
            };

            await repository.AddAsync(order);
            await repository.SaveChangesAsync();

            order.Products = await orderItemService.CreateOrderItemsAsync(order.Id, products);
            await shoppingCartService.EmptyShoppingCartAsync(userId);



            return "Success";
        }
    }
}
