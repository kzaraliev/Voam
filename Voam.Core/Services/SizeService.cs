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

        public async Task<Size?> GetSizeById(int id)
        {
            return await repository.FindAsync<Size>(id);
        }

        public async Task UpdateSizesAsync(int sizeSAmount, int sizeMAmount, int sizeLAmount, int productId)
        {
            var sizesToRemove = await repository.All<Size>().Where(s => s.ProductId == productId).ToListAsync();
            repository.RemoveRange(sizesToRemove);

            await repository.SaveChangesAsync();

            await CreateSizeAsync(sizeSAmount, sizeMAmount, sizeLAmount, productId);
        }
    }
}
