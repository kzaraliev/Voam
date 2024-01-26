using System.ComponentModel.DataAnnotations;
using Voam.Server.Constants;

namespace Voam.Server.Data.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CustomerConstants.FirstnameMaxLength)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(CustomerConstants.LastnameMaxLength)]
        public required string LastName { get; set; }

        [Required]
        [MaxLength(CustomerConstants.EmailMaxLength)]
        [EmailAddress]
        public required string Email { get; set; }
        
        [Required]
        [MaxLength(CustomerConstants.PasswordMaxLength)]
        public required string Password {  get; set; }

        [MaxLength(CustomerConstants.AddressMaxLength)]
        public required string Address { get; set; }

        [MaxLength(CustomerConstants.PhoneNumberMaxLength)]
        public required string PhoneNumber { get; set; }

        public required virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<CartItem> ShoppingCart { get; set; } = new List<CartItem>();
    }
}