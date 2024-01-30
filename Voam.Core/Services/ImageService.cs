using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voam.Core.Contracts;
using Voam.Infrastructure.Data;
using Voam.Infrastructure.Data.Models;

namespace Voam.Core.Services
{
    public class ImageService : IImageService
    {
        private readonly VoamDbContext context;

        public ImageService(VoamDbContext _context)
        {
            context = _context;
        }

        public async Task<bool> CreateImageAsync(ICollection<string> images, int productId)
        {
            List<ProductImage> imagesList = new List<ProductImage>();

            foreach (string imageData in images)
            {
                ProductImage image = new ProductImage()
                {
                    ImageData = Convert.FromBase64String(imageData.Substring("data:image/png;base64,".Length)),
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
