using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Voam.Server.Data.Models;
using Voam.Server.DTOs;
using Voam.Server.Models;

namespace Voam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly VoamDbContext context;

        public ProductController(VoamDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            List<Product> products = context.Products.ToList();
            return StatusCode(StatusCodes.Status200OK, products);
        }

        [HttpGet]
        [Route("GetRecentlyAddedProducts")]
        public IActionResult GetRecentlyAddedProducts()
        {
            List<Product> products = context.Products.OrderByDescending(p => p.Id).Take(3).ToList();
            return StatusCode(StatusCodes.Status200OK, products);
        }

        [HttpGet]
        [Route("GetProductById")]
        public IActionResult GetProductById(int id)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == id);

            return StatusCode(StatusCodes.Status200OK, product);
        }

        [HttpPost]
        [Route("CreateProduct")]
        public IActionResult CreateProduct([FromBody] CreateProductDTO data)
        {
            Product product = new Product()
            {
                Name = data.name,
                Price = data.price,
                Description = data.description,
                IsAvailable = data.isAvailable,
            };

            context.Products.Add(product);
            context.SaveChanges();

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
