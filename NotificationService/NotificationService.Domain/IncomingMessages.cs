using Bets.Abstractions.Domain.DTO;

namespace NotificationService.Domain
{
    /// <summary>
    /// Входящие сообщения
    /// </summary>
    public sealed class IncomingMessages : CreatedEntity
    {
        /// <summary>
        /// Идентификатор получателя сообщения (должен быть одним из Bettors)
        /// </summary>
        public Guid TargetId { get; set; }

        /// <summary>
        /// Идентификатор источника сообщения (должен быть одним из MessageSources)
        /// </summary>
        public Guid SourceId { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Дата и время, до которого сообщение считается актуальным.
        /// Затем можно удалять всё, касающееся этого сообщения из базы вне зависимости от того
        /// , было ли сообщение успешно отправлено конечному получателю.
        /// </summary>
        public DateTime? ActualDate { get; set; }

        /// <summary>
        /// Состояние отправки сообщения
        /// </summary>
        public SendingStates State { get; set; }
    }
}
