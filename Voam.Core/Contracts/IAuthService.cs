﻿using Voam.Core.Models;

namespace Voam.Core.Contracts
{
    public interface IAuthService
    {
        Task<UserPublicModel> GetUserPublicData(string email);
        string GenerateTokenString(LoginUser user);
        Task<bool> Login(LoginUser user);
        Task<bool> RegisterUser(LoginUser user);
    }
}