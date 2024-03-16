using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voam.Core.Contracts;
using Voam.Core.Models.Review;
using Voam.Infrastructure.Data.Models;

namespace Voam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService _reviewService)
        {
            reviewService = _reviewService;
        }

        [AllowAnonymous]
        [HttpGet("GetProductAverageRating")]
        public async Task<IActionResult> GetAverageRatingForProduct(int productId, string userId)
        {
            var avgRating = await reviewService.GetAvgRatingForProductAsync(productId, userId);

            if (avgRating == null)
            {
                return BadRequest();
            }

            return Ok(avgRating);
        }

        [HttpPost("AddRating")]
        public async Task<IActionResult> AddRating(RatingFormModel model)
        {
            if (!await reviewService.AddRatingAsync(model.ProductId, model.UserId, model.Rating))
            {
                return BadRequest();
            }

            return Ok(await reviewService.GetAvgRatingForProductAsync(model.ProductId, model.UserId));
        }
    }
}
