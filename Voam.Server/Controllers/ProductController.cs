using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Voam.Core.Contracts;
using Voam.Core.Models;
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

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            IEnumerable<DisplayProductModel> products = await productService.GetAllProductsAsync();
            return StatusCode(StatusCodes.Status200OK, products);
        }

        [HttpGet]
        [Route("GetRecentlyAddedProducts")]
        public async Task<IActionResult> GetRecentlyAddedProducts()
        {
            IEnumerable<DisplayProductModel> products = await productService.GetRecentlyAddedProductsAsync();
            return StatusCode(StatusCodes.Status200OK, products);
        }

        [HttpGet]
        [Route("GetProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductModel data)
        {
            try
            {
                var product = await productService.CreateFullProductAsync(data, data.images, data.sizeS, data.sizeM, data.sizeL);
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the product.");
            }
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int id, EditProductModel data)
        {
            //Handle update product in the back-end
            //If user add new images - string[]
            //if old images - obj[]
            try
            {
                var product = await productService.UpdateFullProductAsync(id, data, data.images, data.sizeS, data.sizeM, data.sizeL);
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the product.");
            }
        }

        [HttpDelete]
        [Route("DeleteProduct")]
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