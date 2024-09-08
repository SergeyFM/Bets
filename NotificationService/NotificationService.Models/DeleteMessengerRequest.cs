

namespace NotificationService.Models
{
    public sealed class DeleteMessengerRequest
    {
        /// <summary>
        /// Идентификатор мессенджера
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Кто удалил
        /// </summary>
        public string? DeletedBy { get; set; }
    }
}
