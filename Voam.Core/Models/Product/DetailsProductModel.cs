using Voam.Core.Models.ProductImage;
using Voam.Core.Models.Review;
using Voam.Core.Models.Size;
using Voam.Infrastructure.Data.Models;

namespace Voam.Core.Models.Product
{
    public class DetailsProductModel
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public decimal Price { get; set; }

        public ICollection<ProductImageModel> Images { get; set; } = new List<ProductImageModel>();

        public ICollection<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();

        public ICollection<SizeModel> Sizes { get; set; } = new List<SizeModel>();
    }
}
