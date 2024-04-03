using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Voam.Core.Contracts;
using Voam.Core.Models.Identity;

namespace Voam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService _authService)
        {
            authService = _authService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(LoginUser user)
        {
            var result = await authService.RegisterUser(user);
            if (result)
            {
                return await Login(user);
            }

            return BadRequest("Something went wrong");
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

            if (result == true)
            {
                var tokenString = await authService.GenerateTokenString(user);

                var userDetails = await authService.GetUserPublicData(user.Email);

                AuthenticationDetails response = new AuthenticationDetails()
                {
                    AccessToken = tokenString,
                    Email = userDetails.Email,
                    Username = userDetails.Usernam,
                    UserId = userDetails.Id,
                    Roles = userDetails.Roles,
                };
                return Ok(response);
            }

            return Unauthorized(new { message = "Incorrect email or password" });
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
            var result = await authService.GetUserPhoneNumberAsync(id);

            return Ok(JsonSerializer.Serialize(result));
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
