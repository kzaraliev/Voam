using Microsoft.EntityFrameworkCore;
using Voam.Core.Contracts;
using Voam.Core.Models.Review;
using Voam.Infrastructure.Data.Models;
using Voam.Infrastucture.Data.Common;

namespace Voam.Core.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository repository;

        public ReviewService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<bool> AddRating(int productId, string userId, double rating)
        {
            var review = await repository.All<Review>()
                            .Where(r => r.ProductId == productId && r.CustomerId == userId)
                            .FirstOrDefaultAsync();

            if (review != null)
            {
                review.Rating = rating;
                review.ReviewDate = DateTime.Now;
            }
            else
            {
                Review newReview = new Review()
                {
                    CustomerId = userId,
                    ProductId = productId,
                    Rating = rating,
                    ReviewDate = DateTime.Now
                };

                await repository.AddAsync(newReview);
            }
            
            await repository.SaveChangesAsync();

            return true;
        }

        public async Task<RatingModel> GetAvgRatingForProduct(int productId, string userId)
        {

            var reviews = repository.AllReadOnly<Review>()
                            .Where(r => r.ProductId == productId);

            var averageRating = await reviews.Select(r => r.Rating)
                                             .DefaultIfEmpty()
                                             .AverageAsync();

            var userRating = await reviews.Where(r => r.CustomerId == userId)
                                          .Select(r => (double?)r.Rating)
                                          .FirstOrDefaultAsync();

            return new RatingModel
            {
                AverageRating = averageRating,
                UserRating = userRating
            };
        }
    }
}
