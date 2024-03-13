using Voam.Core.Models.Identity;

namespace Voam.Core.Contracts
{
    public interface IAuthService
    {
        Task<UserPublicModel> GetUserPublicData(string email);
        string GenerateTokenString(LoginUser user);
        Task<bool> Login(LoginUser user);
        Task<bool> RegisterUser(LoginUser user);
        Task<OrderInformationModel> GetUserInformation(string id);
    }
}