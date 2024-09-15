
namespace NotificationService.Models
{
    public sealed class MessageSourceResponse
    {
        /// <summary>
        /// Идентификатор сервиса источника (сквозной для всех сервисов)
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Описание источника
        /// </summary>
        public string? Description { get; set; }
    }
}
