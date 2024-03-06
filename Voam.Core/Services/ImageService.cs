using Microsoft.EntityFrameworkCore;
using Voam.Core.Contracts;
using Voam.Infrastructure.Data;
using Voam.Infrastructure.Data.Models;
using Voam.Infrastucture.Data.Common;

namespace Voam.Core.Services
{
    public class ImageService : IImageService
    {
        private readonly IRepository repository;
        private const string Base64ImagePrefix = "data:image/png;base64,";

        public ImageService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<bool> CreateImageAsync(ICollection<string> images, int productId)
        {
            List<ProductImage> imagesList = new List<ProductImage>();

            foreach (string imageData in images)
            {
                if (!imageData.StartsWith(Base64ImagePrefix))
                {
                    throw new ArgumentException("Invalid image format. Only base64 encoded PNG images are accepted.");
                }

                byte[] imageBytes;
                try
                {
                    imageBytes = Convert.FromBase64String(imageData.Substring(Base64ImagePrefix.Length));
                }
                catch (FormatException ex)
                {
                    throw new ArgumentException("Invalid Base64 string.", ex);
                }

                ProductImage image = new ProductImage()
                {
                    ImageData = imageBytes,
                    ProductId = productId,
                };

                imagesList.Add(image);
            }

            if (imagesList.Any())
            {
                await repository.AddRangeAsync(imagesList);
                await repository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task UpdateImageAsync(ICollection<string> images, int productId)
        {
            var imagesToRemove = await repository.All<ProductImage>().Where(s => s.ProductId == productId).ToListAsync();
            repository.RemoveRange(imagesToRemove);

            await repository.SaveChangesAsync();

            await CreateImageAsync(images, productId);
        }
    }
}
