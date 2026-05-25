using SharedLib.DTOs;

namespace EmailNotificationWebhook.Service
{
    public interface IEmailService
    {
        string SendEmail(EmailDTO email);
    }
}
