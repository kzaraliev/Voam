using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Voam.Infrastructure.Data.Constants;

namespace Voam.Infrastructure.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        public required string CustomerId { get; set; }
        public virtual IdentityUser Customer { get; set; } = null!;

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [MaxLength(OrderConstants.FullNameMaxLength)]
        public required string FullName { get; set; }

        [Required]
        [MaxLength(OrderConstants.EmailMaxLength)]
        public required string Email { get; set; }

        [Required]
        [MaxLength(OrderConstants.PhoneNumberMaxLength)]
        public required string PhoneNumber { get; set; }

        [Required]
        [MaxLength(OrderConstants.EcontOfficeAddressMaxLength)]
        public required string Econt { get; set; }

        [Required]
        [MaxLength(OrderConstants.PaymentMethodMaxLength)]
        public required string PaymentMethod { get; set; }

        [Required]
        [MaxLength(OrderConstants.CityNameMaxLength)]
        public required string City { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalPrice { get; set; }

        public virtual IEnumerable<OrderItem> Products { get; set; } = new List<OrderItem>();
    }
}
