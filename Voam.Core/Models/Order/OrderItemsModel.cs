namespace Voam.Core.Models.Order
{
    public class OrderItemsModel
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public int SizeId { get; set; }
    }
}
