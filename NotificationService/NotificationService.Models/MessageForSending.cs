using NotificationService.Models.Enums;

namespace NotificationService.Models
{
    public sealed class MessageForSending
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор получателя сообщения (Bettors)
        /// </summary>
        public Guid TargetId { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Тема сообщения
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Состояние отправки сообщения
        /// </summary>
        public SendingStates State { get; set; }
    }
}
