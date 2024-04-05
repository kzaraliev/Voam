using Microsoft.AspNetCore.Identity;
using Voam.Core.Models.Identity;

namespace Voam.Core.Contracts
{
    public interface IAuthService
    {
        Task<UserPublicModel> GetUserPublicData(string email);
        Task<string> GenerateTokenString(LoginUser user);
        Task<bool> Login(LoginUser user);
        Task<IdentityResult> RegisterUser(LoginUser user);
        Task<OrderInformationModel> GetUserInformation(string id);
        Task<bool> AddRole(string roleName);
        Task<bool> AddUserToRole(string userId, string roleName);
        Task<bool> CheckIsUserWithRole(string userEmail, string roleName);
        Task<string> GetUserPhoneNumberAsync(string userId);
    }
}