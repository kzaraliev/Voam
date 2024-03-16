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
        Task<bool> AddRole(string roleName);
        Task<bool> AddUserToRole(string userId, string roleName);
        Task<bool> CheckIsUserWithRole(string userEmail, string roleName);
    }
}