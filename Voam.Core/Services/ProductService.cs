using Microsoft.EntityFrameworkCore;
using Voam.Core.Contracts;
using Voam.Core.Models;
using Voam.Core.Models.Product;
using Voam.Core.Models.ProductImage;
using Voam.Core.Models.Review;
using Voam.Core.Models.Size;
using Voam.Infrastructure.Data;
using Voam.Infrastructure.Data.Models;
using Voam.Infrastucture.Data.Common;
using static Voam.Core.Utils.Constants;

namespace Voam.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository repository;
        private readonly ISizeService sizeService;
        private readonly IImageService imageService;

        private readonly VoamDbContext context;

        public ProductService(IRepository _repository, ISizeService _sizeService, IImageService _imageService, VoamDbContext _context)
        {
            repository = _repository;
            sizeService = _sizeService;
            imageService = _imageService;
            context = _context;
        }

        public async Task<IEnumerable<DisplayProductModel>> GetAllProductsAsync()
        {
            return await repository.AllReadOnly<Product>()
                .Select(p => new DisplayProductModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Image = p.Images
                        .Select(i => new ProductImageModel()
                        {
                            ImageData = i.ImageData,
                        })
                        .FirstOrDefault(),
                })
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<DisplayProductModel>> GetRecentlyAddedProductsAsync()
        {
            return await repository.AllReadOnly<Product>()
                .Select(p => new DisplayProductModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Image = p.Images
                        .Select(i => new ProductImageModel()
                        {
                            ImageData = i.ImageData,
                        })
                        .FirstOrDefault(),
                })
                .OrderByDescending(p => p.Id)
                .Take(3)
                .ToListAsync();
        }

        public async Task<DetailsProductModel?> GetProductByIdAsync(int id)
        {
            try
            {
                return await repository.AllReadOnly<Product>()
               .Where(p => p.Id == id)
               .Select(p => new DetailsProductModel()
               {
                   Id = p.Id,
                   Name = p.Name,
                   Description = p.Description,
                   Price = p.Price,
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
               .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"A problem occurred while fetching details for product Id {id}.", ex);
            }
        }

        public async Task<DetailsProductModel> CreateFullProductAsync(CreateProductModel data, ICollection<string> images, int sizeSAmount, int sizeMAmount, int sizeLAmount)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Create the product
                    var product = await CreateProductAsync(data);
                    if (product == null)
                    {
                        throw new InvalidOperationException("Failed to create the product.");
                    }

                    // Create sizes
                    var sizeResult = await sizeService.CreateSizeAsync(sizeSAmount, sizeMAmount, sizeLAmount, product.Id);
                    if (!sizeResult)
                    {
                        throw new InvalidOperationException("Failed to create product sizes.");
                    }

                    // Create images
                    var imageResult = await imageService.CreateImageAsync(images, product.Id);
                    if (!imageResult)
                    {
                        throw new InvalidOperationException("Failed to create product images.");
                    }

                    await transaction.CommitAsync();
                    return product;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }


        public async Task<DetailsProductModel?> CreateProductAsync(CreateProductModel data)
        {
            try
            {
                Product product = new Product()
                {
                    Name = data.Name,
                    Description = data.Description,
                    Price = data.Price,
                };

                await repository.AddAsync(product);
                await repository.SaveChangesAsync();

                return await GetProductByIdAsync(product.Id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<DeleteResult> DeleteProductByIdAsync(int id)
        {
            var product = await repository.FindAsync<Product>(id);

            if (product == null)
            {
                return DeleteResult.NotFound;
            }

            repository.Remove(product);
            int saveResult = await repository.SaveChangesAsync();

            return saveResult > 0 ? DeleteResult.Success : DeleteResult.Error;
        }

        public async Task<DetailsProductModel> UpdateFullProductAsync(int id, EditProductModel data, ICollection<string> images, int sizeSAmount, int sizeMAmount, int sizeLAmount)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Update product
                    var product = await UpdateProductAsync(id, data);
                    if (product == null)
                    {
                        throw new InvalidOperationException("Failed to update the product.");
                    }

                    // Update sizes
                    await sizeService.UpdateSizesAsync(sizeSAmount, sizeMAmount, sizeLAmount, product.Id);

                    if (images.Any())
                    {
                        // Update images
                        await imageService.UpdateImageAsync(images, product.Id);
                    }
                                   
                    await transaction.CommitAsync();
                    return product;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<DetailsProductModel> UpdateProductAsync(int id, EditProductModel data)
        {
            try
            {
                var product = await repository.FindAsync<Product>(id);
                if (product == null)
                {
                    throw new InvalidOperationException("Failed to find the product.");
                }

                product.Name = data.Name ?? product.Name;
                product.Description = data.Description ?? product.Description;

                if (data.Price != 0)
                {
                    product.Price = data.Price;
                }
                else
                {
                    product.Price = product.Price;

                }


                await repository.SaveChangesAsync();

                var updatedProduct = new DetailsProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                };

                return updatedProduct;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while updating the product with ID {id}.", ex);
            }
        }
    }
}
