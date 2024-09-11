
namespace NotificationService.Models
{
    public sealed class MessengerUpdateRequest
    {
        /// <summary>
        /// Идентификатор мессенджера
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование мессенджера
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Кто последний раз изменял
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
