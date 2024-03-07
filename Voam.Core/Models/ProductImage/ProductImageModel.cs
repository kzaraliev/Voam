namespace Voam.Core.Models.ProductImage
{
    public class ProductImageModel
    {
        public int Id { get; set; }

        public required byte[] ImageData { get; set; }
    }
}
