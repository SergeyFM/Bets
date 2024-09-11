
namespace NotificationService.Models
{
    public sealed class BettorUpdateRequest
    {
        /// <summary>
        /// Идентификатор игрока (сквозной для всех сервисов)
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Как обращаться к игроку
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Кто последний раз изменял
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
