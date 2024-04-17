using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using Voam.Core.Contracts;
using Voam.Core.Models.Identity;
using static Voam.Core.Constants.CacheConstants;

namespace Voam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IMemoryCache memoryCache;  

        public AuthController(IAuthService _authService, IMemoryCache _memoryCache)
        {
            authService = _authService;
            memoryCache = _memoryCache;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterUser user)
        {
            var result = await authService.RegisterUser(user);
            if (result.Succeeded)
            {
                return await Login(new LoginUser() { Email = user.Email, Password = user.Password});
            }

            if (result.Errors.Any(error => error.Code == "DuplicateEmail"))
            {
                return BadRequest(new { message = "Oops! That email is already connected to an account." });
            }

            if (result.Errors.Any())
            {
                var errorMessage = string.Join("; ", result.Errors.Select(error => error.Description));
                return BadRequest(new { message = $"Something went wrong: {errorMessage}" });
            }

            return BadRequest(new { message = "Something went wrong" });
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await authService.Login(user);

            if (result.IsSuccessful == true)
            {
                var tokenString = await authService.GenerateTokenString(user.Email);
                
                var userDetails = await authService.GetUserPublicData(user.Email);

                AuthenticationDetails response = new AuthenticationDetails()
                {
                    AccessToken = tokenString,
                    RefreshToken = result.RefreshToken,
                    Email = userDetails.Email,
                    Username = userDetails.Usernam,
                    UserId = userDetails.Id,
                    Roles = userDetails.Roles,
                };

                return Ok(response);
            }

            return Unauthorized(new { message = "Incorrect email or password" });
        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel model)
        {
            var loginResult = await authService.RefreshToken(model);
            if (loginResult.IsLogedIn)
            {
                return Ok(loginResult);
            }

            return Unauthorized();
        }

        [HttpGet("GetUserInformation")]
        public async Task<IActionResult> GetUserInformation(string id)
        {
            var result = await authService.GetUserInformation(id);

            return Ok(result);
        }

        [HttpGet("GetUserPhoneNumber")]
        public async Task<IActionResult> GetUserPhoneNumber(string id)
        {
            var phoneNumber = memoryCache.Get<string>(PhoneNumberCacheKey);

            if (phoneNumber == null || phoneNumber.Any() == false)
            {
                phoneNumber = await authService.GetUserPhoneNumberAsync(id);

                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

                memoryCache.Set(PhoneNumberCacheKey, phoneNumber, cacheOptions);
            }

            return Ok(JsonSerializer.Serialize(phoneNumber));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(string roleName)
        {
            var result = await authService.AddRole(roleName);

            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string userId, string roleName)
        {
            var result = await authService.AddUserToRole(userId, roleName);

            return Ok(result);
        }
    }
}
