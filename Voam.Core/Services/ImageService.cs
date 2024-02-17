using Voam.Core.Contracts;
using Voam.Infrastructure.Data;
using Voam.Infrastructure.Data.Models;

namespace Voam.Core.Services
{
    public class ImageService : IImageService
    {
        private readonly VoamDbContext context;
        private const string Base64ImagePrefix = "data:image/png;base64,";

        public ImageService(VoamDbContext _context)
        {
            context = _context;
        }

        public async Task<bool> CreateImageAsync(ICollection<string> images, int productId)
        {
            List<ProductImage> imagesList = new List<ProductImage>();

            foreach (string imageData in images)
            {
                if (!imageData.StartsWith(Base64ImagePrefix))
                {
                    throw new ArgumentException("Invalid image format. Only PNG images are accepted.");
                }

                byte[] imageBytes;
                try
                {
                    imageBytes = Convert.FromBase64String(imageData.Substring(Base64ImagePrefix.Length));
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Invalid Base64 string.");
                }

                ProductImage image = new ProductImage()
                {
                    ImageData = imageBytes,
                    ProductId = productId,
                };

                imagesList.Add(image);
            }

            await context.ProductImages.AddRangeAsync(imagesList);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
