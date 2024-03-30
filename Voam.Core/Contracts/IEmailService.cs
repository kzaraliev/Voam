using Voam.Core.Models.Order;

namespace Voam.Core.Contracts
{
    public interface IEmailService
    {
        void SendEmail(EmailModel request);
    }
}
