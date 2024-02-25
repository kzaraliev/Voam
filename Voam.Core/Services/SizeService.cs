using Microsoft.EntityFrameworkCore;
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
    public class SizeService : ISizeService
    {
        private readonly VoamDbContext context;

        public SizeService(VoamDbContext _context)
        {
            context = _context;
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
                await context.Sizes.AddRangeAsync(sizes);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task UpdateSizesAsync(int sizeSAmount, int sizeMAmount, int sizeLAmount, int productId)
        {
            var sizesToRemove = await context.Sizes.Where(s => s.ProductId == productId).ToListAsync();
            context.Sizes.RemoveRange(sizesToRemove);

            await context.SaveChangesAsync();

            await CreateSizeAsync(sizeSAmount, sizeMAmount, sizeLAmount, productId);
        }
    }
}
