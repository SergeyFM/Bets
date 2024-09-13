
namespace NotificationService.Models
{
    public class BettorAddressResponse
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
        /// Игрок
        /// </summary>
        public BettorResponse Bettor { get; set; }

        /// <summary>
        /// Мессенджер
        /// </summary>
        public MessengerResponse Messenger { get; set; }
    }
}
