using System.ComponentModel.DataAnnotations;
using static Voam.Core.Constants.MessageConstants;


namespace Voam.Core.Models.Order
{
    public class OrderItemCreateModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        public int Quantity { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public char SizeChar { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public required string Name { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public decimal Price { get; set; }
    }
}
