using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using Voam.Core.Contracts;
using Voam.Core.Models.Order;

namespace Voam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IEmailService emailService;

        public TestController(IEmailService _emailService)
        {
            emailService = _emailService;
        }


        [HttpPost("Email")]
        public IActionResult SendEmail(EmailModel request)
        {
            emailService.SendEmail(request);

            return Ok();
        }

        [HttpGet]
        public string Get()
        {
            return "You hit me!";
        }
    }
}
