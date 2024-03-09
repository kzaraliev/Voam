using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voam.Infrastructure.Data.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey(nameof(Size))]
        public int SizeId { get; set; }

        public virtual Size Size { get; set; } = null!;

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;

        [ForeignKey(nameof(ShoppingCart))]
        public int ShoppingCartId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; } = null!;
    }
}