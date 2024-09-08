

namespace NotificationService.Models
{
    public sealed class MessengerResponse
    {
        /// <summary>
        /// Идентификатор мессенджера
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование мессенджера
        /// </summary>
        public string Name { get; set; }
    }
}
