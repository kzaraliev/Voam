using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Voam.Core.Contracts;
using Voam.Core.Models.Identity;
using Voam.Infrastructure.Data.Models;

namespace Voam.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration config;
        private readonly IShoppingCartService shoppingCartService;

        public AuthService(UserManager<ApplicationUser> _userManager, IConfiguration _config, IShoppingCartService _shoppingCartService, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            config = _config;
            shoppingCartService = _shoppingCartService;
            roleManager = _roleManager;
        }

        public async Task<UserPublicModel> GetUserPublicData(string email)
        {
            var identityUser = await userManager.FindByEmailAsync(email);

            if (identityUser == null)
            {
                throw new Exception("No such user");
            }

            var userRoles = await userManager.GetRolesAsync(identityUser);

            var user = new UserPublicModel()
            {
                Id = identityUser.Id,
                Email = identityUser.Email ?? "Error",
                Usernam = $"{identityUser.FirstName} {identityUser.LastName}",
                Roles = userRoles
            };

            return user;
        }

        public async Task<string> GenerateTokenString(string email)
        {
            var identityUser = await userManager.FindByEmailAsync(email);
            if (identityUser == null) throw new Exception("User not found");

            var userRoles = await userManager.GetRolesAsync(identityUser);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,email),
                new Claim(ClaimTypes.Name,email),
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetConfigurationValue("Jwt:Key")));

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: GetConfigurationValue("Jwt:Issuer"),
                audience: GetConfigurationValue("Jwt:Audience"),
                signingCredentials: signingCred);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

        public async Task<LoginTransfer> Login(LoginUser user)
        {
            bool result = true;
            string refreshToken = string.Empty;

            var identityUser = await userManager.FindByEmailAsync(user.Email);
            if (identityUser is null)
            {
                result = false;
            }

            if (result)
            {
                result = await userManager.CheckPasswordAsync(identityUser, user.Password);
                refreshToken = GenerateRefreshTokenString();

                await UpdateUserRefreshTokenAsync(identityUser, refreshToken, 12);
            }

            return new LoginTransfer()
            {
                RefreshToken = refreshToken,
                IsSuccessful = result,
            };
        }

        public async Task<IdentityResult> RegisterUser(RegisterUser user)
        {
            var identityUser = new ApplicationUser { UserName = user.Email, Email = user.Email, PhoneNumber = user.PhoneNumber, FirstName = user.FirstName.Trim(), LastName = user.LastName.Trim() };

            var result = await userManager.CreateAsync(identityUser, user.Password);

            if (result.Succeeded)
            {
                await shoppingCartService.CreateShoppingCartAsync(identityUser.Id);
            }

            return result;
        }

        public async Task<OrderInformationModel> GetUserInformation(string id)
        {
            var identityUser = await userManager.FindByIdAsync(id);

            if (identityUser == null)
            {
                throw new Exception("No such user");
            }

            OrderInformationModel user = new OrderInformationModel()
            {
                Email = identityUser.Email ?? "Error",
                FirstName = identityUser.FirstName ?? "Error",
                LastName = identityUser.LastName ?? "Error",
                PhoneNumber = identityUser.PhoneNumber ?? "Error"
            };

            return user;
        }

        public async Task<bool> AddRole(string roleName)
        {
            if (await roleManager.RoleExistsAsync(roleName) == false)
            {
                var result = await roleManager.CreateAsync(new IdentityRole { Name = roleName, ConcurrencyStamp = Guid.NewGuid().ToString() });
                return result.Succeeded;
            }

            return false;
        }

        public async Task<bool> AddUserToRole(string userId, string roleName)
        {
            if (await roleManager.RoleExistsAsync(roleName))
            {
                var user = await userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    if (await userManager.IsInRoleAsync(user, roleName) == false)
                    {
                        await userManager.AddToRoleAsync(user, roleName);

                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<bool> CheckIsUserWithRole(string userEmail, string roleName)
        {
            if (await roleManager.RoleExistsAsync(roleName))
            {
                var user = await userManager.FindByEmailAsync(userEmail);

                if (user != null)
                {
                    if (await userManager.IsInRoleAsync(user, roleName))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<string> GetUserPhoneNumberAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("No such user");
            }

            return user.PhoneNumber ?? "No phone number provided";
        }

        private string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[64];

            using (var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }

        public async Task<LoginResponse> RefreshToken(RefreshTokenModel model)
        {
            var principal = GetTokenPrincipal(model.JwtToken);

            var response = new LoginResponse();
            if (principal?.Identity?.Name is null)
            {
                return response;
            }

            var identityUser = await userManager.FindByNameAsync(principal.Identity.Name);
            if (identityUser is null || identityUser.RefreshToken != model.RefreshToken || identityUser.RefreshTokenExpiry < DateTime.Now)
                return response;

            response.IsLogedIn = true;
            response.JwtToken = await GenerateTokenString(identityUser.Email);
            response.RefreshToken = GenerateRefreshTokenString();

            await UpdateUserRefreshTokenAsync(identityUser, response.RefreshToken, 12);

            return response;
        }

        private ClaimsPrincipal? GetTokenPrincipal(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetConfigurationValue("Jwt:Key")));

            var validation = new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                ValidateLifetime = false,
                ValidateActor = false,
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }

        private string GetConfigurationValue(string key)
        {
            return config.GetSection(key).Value ?? throw new ArgumentException($"Configuration for {key} is not set properly.");
        }

        private async Task UpdateUserRefreshTokenAsync(ApplicationUser user, string refreshToken, int hoursToAdd)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.Now.AddHours(hoursToAdd);
            await userManager.UpdateAsync(user);
        }
    }
}
