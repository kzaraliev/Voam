using System.ComponentModel.DataAnnotations;
using static Voam.Core.Constants.MessageConstants;

namespace Voam.Core.Models.ShoppingCart
{
    public class ShoppingCartFormModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        public required string UserId { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public int SizeId { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public int Quantity { get; set; }
    }
}
