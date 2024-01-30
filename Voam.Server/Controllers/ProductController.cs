using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Voam.Core.Contracts;
using Voam.Core.Models;
using Voam.Core.Services;
using Voam.Infrastructure.Data.Models;

namespace Voam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ISizeService sizeService;
        private readonly IImageService imageService;

        public ProductController(IProductService _productService, ISizeService _sizeService, IImageService _imageService)
        {
            productService = _productService;
            sizeService = _sizeService;
            imageService = _imageService;
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

            if(product == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        
            return StatusCode(StatusCodes.Status200OK, product);
        }
        
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductModel data)
        {
            var product = await productService.CreateProductAsync(data);
            await sizeService.CreateSizeAsync(data.sizeS, data.sizeM, data.sizeL, product.Id);
            await imageService.CreateImageAsync(data.images, product.Id);
        
            return StatusCode(StatusCodes.Status200OK, product);
        }
    }
}
