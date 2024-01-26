using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voam.Server.Data.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int ProductId { get; set; }

        // Navigation property for Product
        [ForeignKey("ProductId")]
        public required virtual Product Product { get; set; }
    }
}