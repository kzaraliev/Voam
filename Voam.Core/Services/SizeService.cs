using Microsoft.EntityFrameworkCore;
using Voam.Core.Contracts;
using Voam.Core.Models.Size;
using Voam.Infrastructure.Data.Models;
using Voam.Infrastucture.Data.Common;

namespace Voam.Core.Services
{
    public class SizeService : ISizeService
    {
        private readonly IRepository repository;

        public SizeService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<bool> CreateSizeAsync(int sizeSAmount, int sizeMAmount, int sizeLAmount, int productId)
        {
            var sizes = new List<Size>
            {
                new Size { SizeChar = 'S', Quantity = sizeSAmount, ProductId = productId },
                new Size { SizeChar = 'M', Quantity = sizeMAmount, ProductId = productId },
                new Size { SizeChar = 'L', Quantity = sizeLAmount, ProductId = productId }
            }.Where(size => size.Quantity > 0).ToList();

            if (sizes.Any())
            {
                await repository.AddRangeAsync(sizes);
                await repository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Size?> GetSizeByIdAsync(int id)
        {
            return await repository.FindAsync<Size>(id);
        }

        public async Task UpdateSizesAsync(int sizeSAmount, int sizeMAmount, int sizeLAmount, int productId)
        {
            var existingSizes = await repository.All<Size>().Where(s => s.ProductId == productId).ToListAsync();

            // Update existing sizes or create new ones if they don't exist
            UpdateOrCreateSize('S', sizeSAmount, productId, existingSizes);
            UpdateOrCreateSize('M', sizeMAmount, productId, existingSizes);
            UpdateOrCreateSize('L', sizeLAmount, productId, existingSizes);

            await repository.SaveChangesAsync();
        }

        private async void UpdateOrCreateSize(char sizeChar, int quantity, int productId, List<Size> existingSizes)
        {
            var size = existingSizes.FirstOrDefault(s => s.SizeChar == sizeChar);

            if (size != null)
            {
                // Update quantity if size already exists
                size.Quantity = quantity;
            }
            else if (quantity > 0)
            {
                // Create new size if it doesn't exist and quantity is positive
                var newSize = new Size { SizeChar = sizeChar, Quantity = quantity, ProductId = productId };
                await repository.AddAsync(newSize);
            }
        }
    }
}
