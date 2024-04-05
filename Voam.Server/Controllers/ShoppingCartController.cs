using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Voam.Core.Contracts;
using Voam.Core.Models.ShoppingCart;
using Voam.Core.Services;
using static Voam.Core.Utils.Constants;

namespace Voam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

            return Ok(JsonSerializer.Serialize("Success"));
        }

        [HttpPut("ChangeCartItemQuantity")]
        public async Task<IActionResult> ChangeCartItemQuantity(CartItemQuantityUpdateModel model)
        {
            try
            {
                bool updateResult = await shoppingCartService.UpdateCartItemQuantity(model.CartItemId, model.Quantity);

                if (updateResult)
                {
                    return Ok(new { success = true, message = "Quantity updated successfully." });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the cart item.");
            }
        }

        [HttpDelete("DeleteCartItem")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var deleteResult = await shoppingCartService.DeleteCartItemAsyncByIdAsync(id);

            return deleteResult switch
            {
                DeleteResult.NotFound => NotFound($"CartItem with Id {id} not found."),
                DeleteResult.Success => NoContent(),
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Error deleting product")
            };
        }
    }
}
