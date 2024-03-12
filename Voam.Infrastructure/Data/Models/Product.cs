using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Voam.Server.Constants;

namespace Voam.Infrastructure.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ProductConstants.NameMaxLength)]
        public required string Name { get; set; }

        public required string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        public virtual ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual ICollection<Size> Sizes { get; set; } = new List<Size>();
    }
}
