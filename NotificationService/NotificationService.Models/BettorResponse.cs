
namespace NotificationService.Models
{
    public sealed class BettorResponse
    {
        /// <summary>
        /// Идентификатор игрока (сквозной для всех сервисов)
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Как обращаться к игроку
        /// </summary>
        public string Nickname { get; set; }
    }
}
