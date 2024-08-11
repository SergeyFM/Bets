using NotificationService.Domain.Abstractions.DTO;

namespace NotificationService.Domain.Directories
{
    /// <summary>
    /// Справочник игроков
    /// </summary>
    public sealed class Bettors : LaterDeletedEntity
    {
        /// <summary>
        /// Как обращаться к игроку
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Доступные способы связи с игроком
        /// </summary>
        public List<BettorAddresses> BettorMessangers { get; } = [];
    }
}
