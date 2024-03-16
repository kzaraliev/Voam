using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voam.Infrastructure.Data.Models
{
    public class Order
    {
        //Fix magic numbers

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        public required string CustomerId { get; set; }
        public virtual IdentityUser Customer { get; set; } = null!;

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [MaxLength(50)]
        public required string FullName { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string PhoneNumber { get; set; }

        [Required]
        [MaxLength(500)]
        public required string Econt { get; set; }

        [Required]
        [MaxLength(30)]
        public required string PaymentMethod { get; set; }

        [Required]
        [MaxLength(50)]
        public required string City { get; set; }

        public virtual IEnumerable<OrderItem> Products { get; set; } = new List<OrderItem>();
    }
}
