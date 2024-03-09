namespace Voam.Core.Models.ShoppingCart
{
    public class ShoppingCartFormModel
    {
        public required string UserId { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
