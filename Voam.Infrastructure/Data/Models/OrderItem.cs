using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voam.Infrastructure.Data.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        public required virtual Order Order { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public required virtual Product Product { get; set; }
    }
}