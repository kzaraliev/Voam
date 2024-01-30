using Voam.Core.Models;

namespace Voam.Core.Contracts
{
    public interface IProductService
    {
        Task<DetailsProductModel?> CreateProductAsync(CreateProductModel data);
        Task<IEnumerable<DisplayProductModel>> GetAllProductsAsync();
        Task<DetailsProductModel?> GetProductByIdAsync(int id);
        Task<IEnumerable<DisplayProductModel>> GetRecentlyAddedProductsAsync();
    }
}
