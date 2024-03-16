using Microsoft.AspNetCore.Identity;
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

        public OrderService(IRepository _repository, UserManager<IdentityUser> _userManager, IOrderItemService _orderItemService)
        {
            repository = _repository;
            userManager = _userManager;
            orderItemService = _orderItemService;
        }

        public async Task<bool> PlaceOrderAsync(string userId, PlaceOrderModel model)
        {
            var identityUser = await userManager.FindByIdAsync(userId);

            if (identityUser == null)
            {
                return false;
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
            };

            await repository.AddAsync(order);

            order.Products = await orderItemService.CreateOrderItemsAsync(order.Id, model.Products);

            await repository.SaveChangesAsync();

            return true;
        }
    }
}
