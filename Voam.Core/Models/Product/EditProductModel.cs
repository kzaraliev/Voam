using System.ComponentModel.DataAnnotations;
using static Voam.Core.Constants.MessageConstants;
using static Voam.Server.Constants.ProductConstants;

namespace Voam.Core.Models.Product
{
    public class EditProductModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthMessage)]
        public required string Name { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = LengthMessage)]
        public required string Description { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [Range(typeof(decimal), PriceMin, PriceMax, ConvertValueInInvariantCulture = true, ErrorMessage = "Price must be a positive number and less than 1000 lv.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public int SizeS { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public int SizeM { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public int SizeL { get; set; }

        public ICollection<string> Images { get; set; } = new List<string>();
    }
}
