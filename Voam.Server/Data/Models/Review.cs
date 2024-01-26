using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voam.Server.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double Rating { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public required virtual Product Product { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public required virtual Customer Customer { get; set; }
    }
}