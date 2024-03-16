using Voam.Infrastructure.Data.Models;

namespace Voam.Core.Models.Order
{
    public class PlaceOrderModel
    {
        //Add attributes

        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string EcontOffice { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public IEnumerable<CartItem> Products { get; set; } = new List<CartItem>();
    }
}
