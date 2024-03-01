using Microsoft.AspNetCore.Mvc;
using Voam.Core.Contracts;
using Voam.Core.Models;

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
            if (await authService.RegisterUser(user))
            {
                return Ok("Successfuly done");
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
                return Ok(tokenString);
            }

            return BadRequest();
        }

    }
}
