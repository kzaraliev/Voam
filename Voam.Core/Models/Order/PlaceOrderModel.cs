using System.ComponentModel.DataAnnotations;
using static Voam.Core.Constants.MessageConstants;
using static Voam.Infrastructure.Data.Constants.OrderConstants;

namespace Voam.Core.Models.Order
{
    public class PlaceOrderModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength, ErrorMessage = LengthMessage)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength, ErrorMessage = LengthMessage)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength, ErrorMessage = LengthMessage)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(EcontOfficeAddressMaxLength, MinimumLength = EcontOfficeAddressMinLength, ErrorMessage = LengthMessage)]
        public string EcontOffice { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(CityNameMaxLength, MinimumLength = CityNameMinLength, ErrorMessage = LengthMessage)]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(PaymentMethodMaxLength, MinimumLength = PaymentMethodMinLength, ErrorMessage = LengthMessage)]
        public string PaymentMethod { get; set; } = string.Empty;

        public IEnumerable<OrderItemsModel> Products { get; set; } = new List<OrderItemsModel>();

        [Required(ErrorMessage = RequiredMessage)]
        public decimal TotalPrice { get; set; }
    }
}
