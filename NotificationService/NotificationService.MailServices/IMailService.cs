
namespace NotificationService.MailServices
{
    public interface IMailService
    {
        Task<SendingMessageResult> SendAsync(MailData mailData);
    }
}
