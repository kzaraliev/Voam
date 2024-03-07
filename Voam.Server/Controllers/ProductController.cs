using Microsoft.AspNetCore.Mvc;
using Voam.Core.Contracts;
using Voam.Core.Models.Product;
using static Voam.Core.Utils.Constants;

namespace Voam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            IEnumerable<DisplayProductModel> products = await productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("GetRecentlyAddedProducts")]
        public async Task<IActionResult> GetRecentlyAddedProducts()
        {
            IEnumerable<DisplayProductModel> products = await productService.GetRecentlyAddedProductsAsync();
            return Ok(products);
        }

        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductModel data)
        {
            try
            {
                var product = await productService.CreateFullProductAsync(data, data.Images, data.SizeS, data.SizeM, data.SizeL);
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the product.");
            }
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int id, EditProductModel data)
        {
            try
            {
                var product = await productService.UpdateFullProductAsync(id, data, data.Images, data.SizeS, data.SizeM, data.SizeL);
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the product.");
            }
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleteResult = await productService.DeleteProductByIdAsync(id);

            return deleteResult switch
            {
                DeleteResult.NotFound => NotFound($"Product with Id {id} not found."),
                DeleteResult.Success => NoContent(),
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Error deleting product")
            };
        }
    }
}