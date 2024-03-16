using Voam.Core.Models.Review;

namespace Voam.Core.Contracts
{
    public interface IReviewService
    {
        Task<RatingModel> GetAvgRatingForProductAsync(int productId, string userId);
        Task<bool> AddRatingAsync(int productId, string userId, double rating);
    }
}
