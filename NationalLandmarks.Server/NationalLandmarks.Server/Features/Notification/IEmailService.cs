using NationalLandmarks.Server.Data.Models;

namespace NationalLandmarks.Server.Features.Notification
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);

        void sendVerificationEmail(User user, string origin);
    }
}
