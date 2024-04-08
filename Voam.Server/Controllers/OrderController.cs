using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using Voam.Core.Contracts;
using Voam.Core.Models.Order;
using static Voam.Core.Constants.CacheConstants;


namespace Voam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMemoryCache memoryCache;

        public OrderController(IOrderService _orderService, IMemoryCache _memoryCache)
        {
            orderService = _orderService;
            memoryCache = _memoryCache;
        }

        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder(string id, PlaceOrderModel model)
        {
            var result = await orderService.PlaceOrderAsync(id, model);
            return Ok(JsonSerializer.Serialize(result));
        }

        [HttpGet("GetAllOrdersForUser")]
        public async Task<IActionResult> GetAllOrdersForUser(string id, int pageSize, int pageNumber)
        {
            var result = await orderService.GetAllOrdersForUserAsync(id, pageSize, pageNumber);
            return Ok(result);
        }

        [HttpGet("GetAllOrders")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = memoryCache.Get<IEnumerable<OrderHistory>>(AllOrdersHistoryCache);

            if (orders == null || orders.Any() == false)
            {
                orders = await orderService.GetAllOrdersAsync();

                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(3));

                memoryCache.Set(AllOrdersHistoryCache, orders, cacheOptions);
            }

            return Ok(orders);
        }
    }
}
