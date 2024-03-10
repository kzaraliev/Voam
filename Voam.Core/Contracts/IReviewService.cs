using Voam.Core.Models.Review;

namespace Voam.Core.Contracts
{
    public interface IReviewService
    {
        Task<RatingModel> GetAvgRatingForProduct(int productId, string userId);
        Task<bool> AddRating(int productId, string userId, double rating);
    }
}
