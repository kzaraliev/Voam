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
                    accessToken = tokenString,
                    email = userDetails.Email,
                    username = userDetails.Usernam,
                    userId = userDetails.Id
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
        public string email { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string userId { get; set; } = string.Empty;
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
