using Voam.Infrastructure.Data.Models;

namespace Voam.Core.Contracts
{
    public interface ISizeService
    {
        Task<bool> CreateSizeAsync(int sizeSAmount, int sizeMAmount, int sizeLAmount, int productId);
        Task UpdateSizesAsync(int sizeSAmount, int sizeMAmount, int sizeLAmount, int id);
        Task<Size?> GetSizeById(int id);
    }
}
