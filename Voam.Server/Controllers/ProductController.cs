using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Voam.Server.Data.Models;
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
        [Route("GetProducts")]
        public IActionResult GetAllProductsAsync()
        {
            List<Product> products = context.Products.ToList();
            return StatusCode(StatusCodes.Status200OK, products);
        }
    }
}
