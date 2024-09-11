
namespace NotificationService.Models
{
    public sealed class BettorRequest
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
        /// Создатель
        /// </summary>
        public string? CreatedBy { get; set; }
    }
}
