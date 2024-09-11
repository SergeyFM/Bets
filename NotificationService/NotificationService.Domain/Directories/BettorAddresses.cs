using Bets.Abstractions.Domain.DTO;

namespace NotificationService.Domain.Directories
{
    ///// <summary>
    ///// Привязка мессенджеров к игрокам
    ///// </summary>
    //public sealed class BettorMessangers : LaterDeletedEntity

    /// <summary>
    /// Справочник адресов в мессенджерах, по которым можно отправлять сообщения игрокам
    /// </summary>
    public sealed class BettorAddresses : LaterDeletedEntity
    {
        /// <summary>
        /// Адрес для отправки сообщения
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Приоритет использования адреса
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid BettorsId { get; set; }

        /// <summary>
        /// Игрок
        /// </summary>
        public Bettors Bettor { get; set; } = null!;

        /// <summary>
        /// Идентификатор мессенджера
        /// </summary>
        public Guid MessengersId { get; set; }

        /// <summary>
        /// Мессенджер
        /// </summary>
        public Messengers Messenger { get; set; } = null!;
    }
}
