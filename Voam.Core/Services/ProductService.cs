using Microsoft.EntityFrameworkCore;
using System.Text;
using Voam.Core.Contracts;
using Voam.Core.Models;
using Voam.Infrastructure.Data;
using Voam.Infrastructure.Data.Models;

namespace Voam.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly VoamDbContext context;

        public ProductService(VoamDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<DisplayProductModel>> GetAllProductsAsync()
        {
            return await context.Products
                .Select(p => new DisplayProductModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    IsAvailable = p.IsAvailable,
                    Image = p.Images
                        .Select(i => new ProductImageModel()
                        {
                            ImageData = i.ImageData,
                        })
                        .FirstOrDefault(),
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<DisplayProductModel>> GetRecentlyAddedProductsAsync()
        {
            return await context.Products
                .Where(p => p.IsAvailable == true)
                .Select(p => new DisplayProductModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    IsAvailable = p.IsAvailable,
                    Image = p.Images
                        .Select(i => new ProductImageModel()
                        {
                            ImageData = i.ImageData,
                        })
                        .FirstOrDefault(),
                })
                .AsNoTracking()
                .OrderByDescending(p => p.Id)
                .Take(3)
                .ToListAsync();
        }

        public async Task<DetailsProductModel?> GetProductByIdAsync(int id)
        {
            return await context.Products
                .Where(p => p.Id == id)
                .Select(p => new DetailsProductModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    IsAvailable = p.IsAvailable,
                    Images = p.Images
                        .Select(i => new ProductImageModel()
                        {
                            Id = i.Id,
                            ImageData = i.ImageData,
                        })
                        .ToArray(),
                    Reviews = p.Reviews
                        .Select(r => new ReviewModel()
                        {
                            Id = r.Id,
                            Rating = r.Rating,
                        }).ToArray(),
                    Sizes = p.Sizes
                        .Select(s => new SizeModel()
                        {
                            Id = s.Id,
                            Quantity = s.Quantity,
                            SizeChar = s.SizeChar,
                        }).ToArray()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<DetailsProductModel?> CreateProductAsync(CreateProductModel data)
        {
            Product product = new Product()
            {
                Name = data.name,
                Description = data.description,
                Price = data.price,
                IsAvailable = data.isAvailable,
            };

            context.Products.Add(product);
            context.SaveChanges();

            return await GetProductByIdAsync(product.Id);
        }
    }
}
