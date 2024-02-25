using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voam.Core.Contracts
{
    public interface IImageService
    {
        Task<bool> CreateImageAsync(ICollection<string> images, int productId);
        Task UpdateImageAsync(ICollection<string> images, int id);
    }
}
