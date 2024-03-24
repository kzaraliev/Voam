namespace Voam.Core.Models.ShoppingCart
{
    public class CartItemQuantityUpdateModel
    {
        public int CartItemId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
