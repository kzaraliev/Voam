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
            if (sizeSAmount != 0)
            {
                Size sizeS = new Size()
                {
                    SizeChar = 'S',
                    Quantity = sizeSAmount,
                    ProductId = productId
                };
                await context.Sizes.AddAsync(sizeS);
            }

            if (sizeMAmount != 0)
            {
                Size sizeM = new Size()
                {
                    SizeChar = 'M',
                    Quantity = sizeMAmount,
                    ProductId = productId
                };
                await context.Sizes.AddAsync(sizeM);
            }

            if (sizeLAmount != 0)
            {
                Size sizeL = new Size()
                {
                    SizeChar = 'L',
                    Quantity = sizeLAmount,
                    ProductId = productId
                };
                await context.Sizes.AddAsync(sizeL);
            }

            await context.SaveChangesAsync();

            return true;
        }
    }
}
