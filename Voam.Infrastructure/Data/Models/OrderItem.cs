using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voam.Infrastructure.Data.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public char SizeChar { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; } = null!;
    }
}
