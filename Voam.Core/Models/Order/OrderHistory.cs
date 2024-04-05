namespace Voam.Core.Models.Order
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public required string OrderDate { get; set; }
        public required string EcontAddress { get; set; }
        public required string PaymentMethod { get; set;}
        public required string City { get; set;}
        public decimal TotalPrice { get; set; }
        public string FullName { get; set;} = string.Empty;
        public IEnumerable<OrderItemHistory> Products { get; set; } = new List<OrderItemHistory>();
    }
}
