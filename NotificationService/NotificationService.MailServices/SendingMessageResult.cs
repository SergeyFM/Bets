
namespace NotificationService.MailServices
{
    public sealed class SendingMessageResult
    {
        private SendingMessageResult(bool isSending, string? error)
        {
            IsSending = isSending;
            Error = error;
        } 

        public bool IsSending { get; private set; }
        public string? Error { get; private set; }

        public static SendingMessageResult CreateSuccess()
        {
            return new SendingMessageResult(true, null);
        }

        public static SendingMessageResult CreateFailure(Exception ex)
        {
            return new SendingMessageResult(false, ex.ToString());
        }
    }
}
