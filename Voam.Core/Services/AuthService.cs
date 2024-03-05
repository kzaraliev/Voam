using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Voam.Core.Contracts;
using Voam.Core.Models;

namespace Voam.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration config;

        public AuthService(UserManager<IdentityUser> _userManager, IConfiguration _config)
        {
            userManager = _userManager;
            config = _config;
        }

        public async Task<UserPublicModel> GetUserPublicData(string email)
        {
            var identityUser = await userManager.FindByEmailAsync(email);

            if (identityUser == null)
            {
                throw new Exception("No such user");
            }

            var user = new UserPublicModel()
            {
                Id = identityUser.Id,
                Email = identityUser.Email ?? "Error",
                Usernam = Split(identityUser.UserName ?? "Error"),
            };

            return user;
        }

        public string GenerateTokenString(LoginUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,"Admin"),
            };

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
            var identityUser = new IdentityUser { UserName = $"{user.FirstName}{user.LastName}", Email = user.Email, PhoneNumber = user.PhoneNumber };

            var result = await userManager.CreateAsync(identityUser, user.Password);
            return result.Succeeded;
        }

        private string Split(string input)
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
    }
}
