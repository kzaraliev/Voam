using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voam.Infrastructure.Data.Models
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
        public string CustomerId { get; set; } = string.Empty;

        public required virtual IdentityUser Customer { get; set; }
    }
}