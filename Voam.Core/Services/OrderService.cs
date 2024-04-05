using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Voam.Core.Contracts;
using Voam.Core.Models.Order;
using Voam.Infrastructure.Data.Models;
using Voam.Infrastucture.Data.Common;

namespace Voam.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository repository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrderItemService orderItemService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IEmailService emailService;

        public OrderService(IRepository _repository, UserManager<ApplicationUser> _userManager, IOrderItemService _orderItemService, IShoppingCartService _shoppingCartService, IEmailService _emailService)
        {
            repository = _repository;
            userManager = _userManager;
            orderItemService = _orderItemService;
            shoppingCartService = _shoppingCartService;
            emailService = _emailService;
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
                    Quantity = item.Quantity,
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

            StringBuilder sb = new StringBuilder();

            foreach (var item in order.Products)
            {
                sb.AppendLine($"{item.Name} - {item.SizeChar} x {item.Quantity};");
            }

            string orderDate = order.OrderDate.ToString("dd/MM/yyyy");

            EmailModel customerEmail = new EmailModel()
            {
                To = order.Email,
                Subject = $"Order Confirmation from Voam - {order.Id}",
                Body = $"<p><span style=\"font-size: 14pt;\">Dear {identityUser.FirstName} {identityUser.LastName},</span></p>\r\n<p><span style=\"font-size: 14pt;\">Thank you for choosing Voam! We are delighted to <strong>confirm your recent order</strong> with us. Below, you will find the <strong>details of your purchase</strong>:</span></p>\r\n<p>&nbsp;</p>\r\n<p><span style=\"font-size: 14pt;\"><strong>Order Number</strong>: {order.Id}</span></p>\r\n<p><span style=\"font-size: 14pt;\"><strong>Order Date</strong>:<strong> </strong>{orderDate }</span></p>\r\n<p><span style=\"font-size: 14pt;\"><strong>Shipping Address</strong> (Econt Office): {order.Econt}</span></p>\r\n<p>&nbsp;</p>\r\n<p><span style=\"font-size: 14pt;\"><strong>Order Summary</strong>:</span></p>\r\n<p><span style=\"font-size: 14pt;\">{sb.ToString().TrimEnd()}</span></p>\r\n<p>&nbsp;</p>\r\n<p><span style=\"font-size: 14pt;\"><strong>Total Price</strong>:<strong> </strong>{order.TotalPrice} lv.</span></p>\r\n<p><span style=\"font-size: 14pt;\"><strong>Payment Method</strong>: {order.PaymentMethod}</span></p>\r\n<p>&nbsp;</p>\r\n<p><span style=\"font-size: 14pt;\">If you have any <strong>questions </strong>or need further assistance with your order, please do not hesitate to <strong>contact us</strong> - <a href=\"https://mail.google.com/mail/u/0/?fs=1&amp;tf=cm&amp;to=voaminfo@gmail.com\" target=\"_blank\" rel=\"noopener\">voaminfo@gmail.com</a>.</span></p>\r\n<p><span style=\"font-size: 14pt;\"><strong>Thank you for choosing Voam</strong> for your fashion needs. We truly appreciate your support. We look forward to providing you with <strong>top-quality</strong> clothing that is as unique and stylish <strong>as you are</strong>!</span></p>\r\n<p>&nbsp;</p>\r\n<p><span style=\"font-size: 14pt;\"><strong>Best regards</strong>,</span></p>\r\n<p><span style=\"font-size: 14pt;\">Voam Clothing</span></p>\r\n<p>&nbsp;</p>\r\n<p><span style=\"font-size: 14pt;\"><strong>Where you can find us</strong>:</span></p>\r\n<p><span style=\"font-size: 14pt;\">Website: <a href=\"https://voamclothing.com/\">voamclothing.com</a></span></p>\r\n<p><span style=\"font-size: 14pt;\">Email: <a href=\"https://mail.google.com/mail/u/0/?fs=1&amp;tf=cm&amp;to=voaminfo@gmail.com\" target=\"_blank\" rel=\"noopener\">voaminfo@gmail.com</a></span></p>\r\n<p><span style=\"font-size: 14pt;\">Follow us on: <a href=\"https://www.tiktok.com/@voamclothing\">TikTok</a>, <a href=\"https://www.instagram.com/voamclothing_/\">Instagram</a></span></p>"
            };

            emailService.SendEmail(customerEmail);

            return "Success";
        }

        public async Task<IEnumerable<OrderHistory>> GetAllOrdersForUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("No such user");
            }

            return await repository.AllReadOnly<Order>()
                .Where(o => o.CustomerId == userId)
                .Select(o => new OrderHistory()
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate.ToString("dd.MM.yyyy"),
                    EcontAddress = o.Econt,
                    PaymentMethod = o.PaymentMethod,
                    City = o.City,
                    TotalPrice = o.TotalPrice,
                    Products = o.Products
                        .Select(p => new OrderItemHistory()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Quantity = p.Quantity,
                            SizeChar = p.SizeChar,
                        })
                        .ToList()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderHistory>> GetAllOrdersAsync()
        {
            return await repository.AllReadOnly<Order>()
                .Select(o => new OrderHistory()
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate.ToString("dd.MM.yyyy"),
                    EcontAddress = o.Econt,
                    PaymentMethod = o.PaymentMethod,
                    City = o.City,
                    TotalPrice = o.TotalPrice,
                    Products = o.Products
                        .Select(p => new OrderItemHistory()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Quantity = p.Quantity,
                            SizeChar = p.SizeChar,
                        })
                        .ToList()
                })
                .ToListAsync();
        }
    }
}
