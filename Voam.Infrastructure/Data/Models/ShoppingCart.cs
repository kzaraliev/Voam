using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voam.Infrastructure.Data.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        public required string CustomerId { get; set; }

        public virtual ApplicationUser Customer { get; set; } = null!;

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}