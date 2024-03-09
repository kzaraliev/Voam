using Microsoft.AspNetCore.Mvc;
using Voam.Core.Contracts;
using Voam.Core.Models.ShoppingCart;

namespace Voam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService _shoppingCartService)
        {
            shoppingCartService = _shoppingCartService;
        }

        [HttpGet("GetShoppingCart")]
        public async Task<IActionResult> GetShoppingCart(string userId)
        {
            var shoppingCart = await shoppingCartService.GetShoppingCartAsync(userId);
            return Ok(shoppingCart);
        }

        [HttpPut("AddCartItem")]
        public async Task<IActionResult> AddCartItem(ShoppingCartFormModel model)
        {
            if (!await shoppingCartService.AddCartItemAsync(model.UserId, model.ProductId, model.SizeId, model.Quantity))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
