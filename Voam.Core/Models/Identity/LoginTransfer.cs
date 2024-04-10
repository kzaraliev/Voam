using Voam.Infrastructure.Data.Models;

namespace Voam.Core.Models.Identity
{
    public class LoginTransfer
    {
        public bool IsSuccessful { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
}
