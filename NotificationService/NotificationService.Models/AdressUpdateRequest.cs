
namespace NotificationService.Models
{
    public sealed class AddressUpdateRequest
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
        /// Кто последний раз изменял
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
