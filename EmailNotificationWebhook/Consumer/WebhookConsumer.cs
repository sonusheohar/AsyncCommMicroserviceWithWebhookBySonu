using MassTransit;
using SharedLib.DTOs;

namespace EmailNotificationWebhook.Consumer
{
    public class WebhookConsumer(HttpClient _client) : IConsumer<EmailDTO>
    {
        public async Task Consume(ConsumeContext<EmailDTO> context)
        {
            var result = await _client.PostAsJsonAsync("https://localhost:7224/email-webhook", new EmailDTO(context.Message.Title, context.Message.Content));
            result.EnsureSuccessStatusCode();
        }
    }
}
