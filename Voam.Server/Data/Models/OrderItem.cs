using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voam.Server.Data.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        // Navigation properties
        [ForeignKey("OrderId")]
        public required virtual Order Order { get; set; }

        [ForeignKey("ProductId")]
        public required virtual Product Product { get; set; }
    }
}