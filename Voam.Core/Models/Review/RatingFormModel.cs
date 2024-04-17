using System.ComponentModel.DataAnnotations;
using static Voam.Core.Constants.MessageConstants;

namespace Voam.Core.Models.Review
{
    public class RatingFormModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        public required string UserId { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public double Rating { get; set; }
    }
}
