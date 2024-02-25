using Voam.Core.Models;
using Voam.Infrastructure.Data.Models;
using static Voam.Core.Utils.Constants;

namespace Voam.Core.Contracts
{
    public interface IProductService
    {
        Task<DetailsProductModel> CreateFullProductAsync(CreateProductModel data, ICollection<string> images, int sizeS, int sizeM, int sizeL);
        Task<DetailsProductModel?> CreateProductAsync(CreateProductModel data);
        Task<DeleteResult> DeleteProductByIdAsync(int id);
        Task<IEnumerable<DisplayProductModel>> GetAllProductsAsync();
        Task<DetailsProductModel?> GetProductByIdAsync(int id);
        Task<IEnumerable<DisplayProductModel>> GetRecentlyAddedProductsAsync();
        Task<DetailsProductModel> UpdateFullProductAsync(int id, EditProductModel data, ICollection<string> images, int sizeS, int sizeM, int sizeL);
    }
}
