namespace Voam.Core.Models.Review
{
    public class RatingFormModel
    {
        public required string UserId { get; set; }
        public int ProductId { get; set; }
        public double Rating { get; set; }
    }
}
