namespace NotificationService.Models
{
    public sealed class IncomingMessageRequest
    {
        /// <summary>
        /// Отправитель (наименование или идентификатор класса или пользователя)
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Идентификатор получателя сообщения (Bettors)
        /// </summary>
        public Guid TargetId { get; set; }

        /// <summary>
        /// Идентификатор источника сообщения (сервиса)
        /// </summary>
        public Guid SourceId { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Тема сообщения
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Дата и время, до которого сообщение считается актуальным.
        /// После этой даты отправлять сообщение конечному получателю не имеет смысла.
        /// </summary>
        public DateTime? ActualDate { get; set; }
    }
}
