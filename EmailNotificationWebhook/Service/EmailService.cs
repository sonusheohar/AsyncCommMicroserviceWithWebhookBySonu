using MimeKit;
using MailKit.Security;
using SharedLib.DTOs;
using MailKit.Net.Smtp;   

namespace EmailNotificationWebhook.Service
{
    public class EmailService : IEmailService
    {
        public string SendEmail(EmailDTO email)
        {
            var _email = new MimeMessage();
            _email.From.Add(MailboxAddress.Parse(""));   // ✅ Replace with actual sender like company email name
            _email.To.Add(MailboxAddress.Parse(""));                 // ✅ Use recipient from DTO
            _email.Subject = email.Title;
            _email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = email.Content
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.example.com", 587, SecureSocketOptions.StartTls); // ✅ Replace with SMTP host
            smtp.Authenticate("smtp-username", "smtp-password", CancellationToken.None);                 // ✅ Add credentials
            smtp.Send(_email);                                                   // ✅ Actually send
            smtp.Disconnect(true);

            return "Email sent successfully!";
        }
    }
}
