using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Voam.Core.Contracts;
using Voam.Core.Models.Order;

namespace Voam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService _orderService)
        {
            orderService = _orderService;
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
            var result = await orderService.GetAllOrdersAsync();
            return Ok(result);
        }
    }
}
