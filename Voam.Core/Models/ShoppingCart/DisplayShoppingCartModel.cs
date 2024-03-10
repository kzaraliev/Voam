namespace Voam.Core.Models.ShoppingCart
{
    public class DisplayShoppingCartModel
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public required string CustomerId { get; set; }

        public virtual ICollection<CartItemsModel> CartItems { get; set; } = new List<CartItemsModel>();
    }
}
