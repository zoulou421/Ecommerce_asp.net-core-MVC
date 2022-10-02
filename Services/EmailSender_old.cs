/*using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace OnlineShop.Models
{
    public class EmailSender_old:IEmailSender
    {
        public AuthMessageSenderOptions Options { get; }

        public EmailSender_old(IOptions<AuthMessageSenderOptions> optionsAccessor )
        {
            Options = optionsAccessor.Value;
        }

        
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(Options.SendGridKey,subject,htmlMessage,email);
        }

        private Task Execute(string? ApiKey, string subject, string htmlMessage, string email)
        {
            var client = new SendGridClient(ApiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("no-reply@formationkilo.com", "OnlineShop"),
                Subject= subject,
                PlainTextContent =htmlMessage,
                HtmlContent=htmlMessage
            };
            msg.AddTo(new EmailAddress(email));
            //Desable click Tracking
            msg.SetClickTracking(false, false);
            return client.SendEmailAsync(msg);
        }
    }
}*/
