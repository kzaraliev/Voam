using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Voam.Infrastructure.Data.Constants;

namespace Voam.Infrastructure.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(ReviewConstants.ReviewMinNumber, ReviewConstants.ReviewMaxNumber)]
        public double Rating { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;

        [ForeignKey(nameof(Customer))]
        public string CustomerId { get; set; } = string.Empty;

        public virtual ApplicationUser Customer { get; set; } = null!;
    }
}