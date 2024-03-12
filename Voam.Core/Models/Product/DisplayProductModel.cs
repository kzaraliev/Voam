using Voam.Core.Models.ProductImage;

namespace Voam.Core.Models.Product
{
    public class DisplayProductModel
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public decimal Price { get; set; }

        public ProductImageModel? Image { get; set; } = null!;
    }
}
