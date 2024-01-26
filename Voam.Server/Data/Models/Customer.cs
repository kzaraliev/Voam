using System.ComponentModel.DataAnnotations;

namespace Voam.Server.Data.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public required string Email { get; set; }

        [MaxLength(255)]
        public required string Address { get; set; }

        [MaxLength(20)]
        public required string PhoneNumber { get; set; }

        // Navigation property for Orders
        public required virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        // New property for shopping cart
        public virtual ICollection<CartItem> ShoppingCart { get; set; } = new List<CartItem>();
    }
}