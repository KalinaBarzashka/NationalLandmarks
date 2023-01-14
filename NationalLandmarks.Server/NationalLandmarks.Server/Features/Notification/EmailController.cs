using Microsoft.AspNetCore.Mvc;

namespace NationalLandmarks.Server.Features.Notification
{
    public class EmailController : ApiController
    {
        private readonly IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost]
        public ActionResult SendEmail(string to, string subject, string html)
        {
            this.emailService.Send(to, subject, html);
            return Ok();
        }
    }
}
