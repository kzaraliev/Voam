﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Voam.Core.Contracts;
using Voam.Core.Models.Product;
using static Voam.Core.Utils.Constants;
using static Voam.Core.Constants.CacheConstants;

namespace Voam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMemoryCache memoryCache;

        public ProductController(IProductService _productService, IMemoryCache _memoryCache)
        {
            productService = _productService;
            memoryCache = _memoryCache;
        }

        [AllowAnonymous]
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = memoryCache.Get<IEnumerable<DisplayProductModel>>(ProductsCacheKey);

            if (products == null || products.Any() == false)
            {
                products = await productService.GetAllProductsAsync();

                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(60));

                memoryCache.Set(ProductsCacheKey, products, cacheOptions);
            }

            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("GetRecentlyAddedProducts")]
        public async Task<IActionResult> GetRecentlyAddedProducts()
        {

            var products = memoryCache.Get<IEnumerable<DisplayProductModel>>(RecntProductsCacheKey);

            if (products == null || products.Any() == false)
            {
                products = await productService.GetAllProductsAsync();

                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(60));

                memoryCache.Set(RecntProductsCacheKey, products, cacheOptions);
            }

            products = await productService.GetRecentlyAddedProductsAsync();
            return Ok(products);
        }

        [AllowAnonymous]
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