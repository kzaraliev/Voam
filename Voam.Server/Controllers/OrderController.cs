﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return Ok(result);
        }

        [HttpGet("GetAllOrdersForUser")]
        public async Task<IActionResult> GetAllOrdersForUser(string id)
        {
            var result = await orderService.GetAllOrdersForUserAsync(id);
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
