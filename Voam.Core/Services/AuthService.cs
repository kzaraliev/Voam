using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

            var userRoles =  await userManager.GetRolesAsync(identityUser);

            var user = new UserPublicModel()
            {
                Id = identityUser.Id,
                Email = identityUser.Email ?? "Error",
                Usernam = $"{identityUser.FirstName} {identityUser.LastName}",
                Roles = userRoles
            };

            return user;
        }

        public async Task<string> GenerateTokenString(LoginUser user)
        {
            var identityUser = await userManager.FindByEmailAsync(user.Email);
            if (identityUser == null) throw new Exception("User not found");

            var userRoles = await userManager.GetRolesAsync(identityUser);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:Key").Value ?? throw new ArgumentException("JWT Key is not configured properly.")));

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: config.GetSection("Jwt:Issuer").Value,
                audience: config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

        public async Task<bool> Login(LoginUser user)
        {
            var identityUser = await userManager.FindByEmailAsync(user.Email);
            if (identityUser is null)
            {
                return false;
            }

            return await userManager.CheckPasswordAsync(identityUser, user.Password);
        }

        public async Task<bool> RegisterUser(LoginUser user)
        {
            var identityUser = new ApplicationUser { UserName = user.Email, Email = user.Email, PhoneNumber = user.PhoneNumber, FirstName = user.FirstName.Trim(), LastName = user.LastName.Trim()};

            var result = await userManager.CreateAsync(identityUser, user.Password);

            if (result.Succeeded)
            {
                await shoppingCartService.CreateShoppingCartAsync(identityUser.Id);
            }

            return result.Succeeded;
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
    }
}
