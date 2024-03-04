using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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
                AuthenticationDetails response = new AuthenticationDetails()
                {
                    accessToken = tokenString,
                };
                return Ok(response);
            }

            var errorResponse = new ErrorResponse(401, "Unauthorized", "Incorrect email or password.");

            return StatusCode(errorResponse.Status, errorResponse);
        }

    }
    public class AuthenticationDetails
    {
        public string accessToken { get; set; } = string.Empty;
    }

    public class ErrorResponse
    {
        public int Status { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }

        public ErrorResponse(int status, string error, string message)
        {
            Status = status;
            Error = error;
            Message = message;
        }
    }
}
