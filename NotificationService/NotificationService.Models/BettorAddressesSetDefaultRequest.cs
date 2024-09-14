
namespace NotificationService.Models
{
    public sealed class BettorAddressesSetDefaultRequest
    {
        /// <summary>
        /// Идентификатор адреса
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Кто последний раз изменял
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
