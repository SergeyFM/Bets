
namespace NotificationService.Models
{
    public sealed class BettorAddressesRequest
    {
        /// <summary>
        /// Адрес для отправки сообщения
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Приоритет использования адреса
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid BettorId { get; set; }

        /// <summary>
        /// Идентификатор мессенджера
        /// </summary>
        public Guid MessengerId { get; set; }

        /// <summary>
        /// Создатель
        /// </summary>
        public string? CreatedBy { get; set; }
    }
}
