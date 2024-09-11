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

        //Информация в этом справочнике об игроках
        //, с которыми возможна связь по конкретному мессенджеру
        //, пожалуй, не нужна
        //public List<BettorAddresses> BettorAddresses { get; } = [];
    }
}
