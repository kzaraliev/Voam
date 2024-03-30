using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using Voam.Core.Contracts;
using Voam.Core.Models.Order;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

namespace Voam.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration config;

        public EmailService(IConfiguration _config)
        {
            config = _config;
        }

        public void SendEmail(EmailModel request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(config.GetSection("EmailUsername").Value, config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
