using System.ComponentModel.DataAnnotations;
using static Voam.Core.Constants.MessageConstants;

namespace Voam.Core.Models.ShoppingCart
{
    public class CartItemQuantityUpdateModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        public int CartItemId { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public int SizeId { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public int Quantity { get; set; }
    }
}
