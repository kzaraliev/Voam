using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Voam.Core.Contracts;
using Voam.Core.Models.Identity;

namespace Voam.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration config;
        private readonly IShoppingCartService shoppingCartService;

        public AuthService(UserManager<IdentityUser> _userManager, IConfiguration _config, IShoppingCartService _shoppingCartService, RoleManager<IdentityRole> _roleManager)
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
                Usernam = SplitUsername(identityUser.UserName ?? "Error"),
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

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:Key").Value));

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
            var identityUser = new IdentityUser { UserName = CombineFirstAndLastName(user.FirstName, user.LastName), Email = user.Email, PhoneNumber = user.PhoneNumber };

            var result = await userManager.CreateAsync(identityUser, user.Password);

            if (result.Succeeded)
            {
                await shoppingCartService.CreateShoppingCartAsync(identityUser.Id);
            }

            return result.Succeeded;
        }

        private string SplitUsername(string input)
        {
            int uppercaseCount = 0;

            // Iterate through each character in the input string
            for (int i = 0; i < input.Length; i++)
            {
                // Check if the character is an uppercase letter
                if (Char.IsUpper(input[i]))
                {
                    uppercaseCount++; // Increment the uppercase letter count

                    // If this is the second uppercase letter, split the string here
                    if (uppercaseCount == 2)
                    {
                        // Use string.Substring to split the string and insert a space
                        return input.Substring(0, i) + " " + input.Substring(i);
                    }
                }
            }

            // Return the original input if no split is performed
            return input;
        }

        private string CombineFirstAndLastName(string firstName, string lastName)
        {
            string result = "";

            string firstNameToLower = firstName.ToLower(); // Convert the entire string to lowercase first
            string lastNameToLower = lastName.ToLower(); // Convert the entire string to lowercase first

            if (!string.IsNullOrEmpty(firstNameToLower) && !string.IsNullOrEmpty(lastNameToLower))
            {
                // Make the first character uppercase
                result = (char.ToUpper(firstNameToLower[0]) + firstNameToLower.Substring(1)) + (char.ToUpper(lastNameToLower[0]) + lastNameToLower.Substring(1));
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

            string fullName = SplitUsername(identityUser.UserName ?? "ErrorPetrov");

            OrderInformationModel user = new OrderInformationModel()
            {
                Email = identityUser.Email ?? "Error",
                FirstName = fullName.Split(" ")[0],
                LastName = fullName.Split(" ")[1],
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
