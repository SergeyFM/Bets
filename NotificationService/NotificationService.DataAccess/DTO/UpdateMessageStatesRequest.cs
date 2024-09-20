using NotificationService.Domain;

namespace NotificationService.DataAccess.DTO
{
    public sealed class UpdateMessageStatesRequest
    {
        /// <summary>
        /// Идентификаторы сообщений для обновления статуса
        /// </summary>
        public IEnumerable<Guid> Ids { get; set; } = Array.Empty<Guid>();

        /// <summary>
        /// Новый статус
        /// </summary>
        public SendingStates State { get; set; }
    }
}
