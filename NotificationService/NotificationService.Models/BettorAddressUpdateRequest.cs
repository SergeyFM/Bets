
namespace NotificationService.Models
{
    public sealed class BettorAddressUpdateRequest
    {
        /// <summary>
        /// Идентификатор адреса
        /// </summary>
        public Guid Id { get; set; }

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
        /// Кто последний раз изменял
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
