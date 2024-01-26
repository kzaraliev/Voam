using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voam.Server.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public double Rating { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; }

        // Navigation properties
        [ForeignKey("ProductId")]
        public required virtual Product Product { get; set; }

        [ForeignKey("CustomerId")]
        public required virtual Customer Customer { get; set; }
    }
}