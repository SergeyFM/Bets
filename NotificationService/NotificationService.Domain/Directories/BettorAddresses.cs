﻿using Bets.Abstractions.Domain.DTO;

namespace NotificationService.Domain.Directories
{
    /// <summary>
    /// Справочник адресов в мессенджерах, по которым можно отправлять сообщения игрокам
    /// </summary>
    public class BettorAddresses : LaterDeletedEntity
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
        public Guid BettorId { get; set; }

        /// <summary>
        /// Игрок
        /// </summary>
        public required virtual Bettors Bettor { get; set; }

        /// <summary>
        /// Идентификатор мессенджера
        /// </summary>
        public Guid MessengerId { get; set; }

        /// <summary>
        /// Мессенджер
        /// </summary>
        public required virtual Messengers Messenger { get; set; }
    }
}
