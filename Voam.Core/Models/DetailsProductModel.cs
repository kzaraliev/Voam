using Voam.Infrastructure.Data.Models;

namespace Voam.Core.Models
{
    public class DetailsProductModel
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public virtual ICollection<ProductImageModel> Images { get; set; } = new List<ProductImageModel>();

        public virtual ICollection<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();

        public virtual ICollection<SizeModel> Sizes { get; set; } = new List<SizeModel>();
    }
}
