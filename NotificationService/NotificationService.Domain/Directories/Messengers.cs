using Bets.Abstractions.Domain.DTO;

namespace NotificationService.Domain.Directories
{
    /// <summary>
    /// Справочник мессенджеров (способов связи)
    /// </summary>
    public sealed class Messengers : LaterDeletedEntity
    {
        /// <summary>
        /// Наименование мессенджера
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Используется только для определения связи с адресами игроков. 
        /// Нет смысла где-то выводить или ещё как-то использовать этот список.
        /// </summary>
        public List<BettorAddresses> BettorAddresses { get; } = [];
    }
}
