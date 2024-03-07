using Microsoft.AspNetCore.Mvc;
using Voam.Core.Contracts;
using Voam.Core.Models.Identity;

namespace Voam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService _authService)
        {
            authService = _authService;
        }

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
                var tokenString = authService.GenerateTokenString(user);

                var userDetails = await authService.GetUserPublicData(user.Email);

                AuthenticationDetails response = new AuthenticationDetails()
                {
                    AccessToken = tokenString,
                    Email = userDetails.Email,
                    Username = userDetails.Usernam,
                    UserId = userDetails.Id
                };
                return Ok(response);
            }

            return Unauthorized(new {message = "Incorrect email or password" });
        }

    }
}
