using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voam.Server.Data.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required byte[] ImageData { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public required virtual Product Product { get; set; }
    }
}