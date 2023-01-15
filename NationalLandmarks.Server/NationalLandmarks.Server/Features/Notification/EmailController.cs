namespace NationalLandmarks.Server.Features.Notification
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class EmailController : ApiController
    {
        private readonly IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult SendEmail(string to, string subject, string html)
        {
            this.emailService.Send(to, subject, html);
            return Ok();
        }
    }
}
