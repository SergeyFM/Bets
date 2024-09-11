
namespace NotificationService.Models
{
    public sealed class MessengerRequest
    {
        /// <summary>
        /// Наименование мессенджера
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Создатель
        /// </summary>
        public string? CreatedBy { get; set; }
    }
}
