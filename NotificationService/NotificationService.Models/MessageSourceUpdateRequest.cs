
namespace NotificationService.Models
{
    public sealed class MessageSourceUpdateRequest
    {
        /// <summary>
        /// Идентификатор сервиса источника (сквозной для всех сервисов)
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Описание источника
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Кто последний раз изменял
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
