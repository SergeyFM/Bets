﻿using Bets.Abstractions.Domain.DTO;

namespace BetsService.Domain
{
    /// <summary>
    /// События, на исходы которых можно делать ставки
    /// </summary>
    public sealed class Events : LaterDeletedEntity
    {
        /// <summary>
        /// Описание события
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Дата и время начала события
        /// </summary>
        public DateTime EventStartTime { get; set; }

        /// <summary>
        /// Дата и время окончания приема ставок на событие
        /// </summary>
        public DateTime BetsEndTime { get; set; }

        /// <summary>
        /// Верно, если событие завершено
        /// </summary>
        public bool IsOver { get; set; }

        /// <summary>
        /// Верно, если событие отменено
        /// </summary>
        public bool IsCanceled { get; set; }

        /// <summary>
        /// Возможные исходы события
        /// </summary>
        public List<EventOutcomes> EventOutcomes { get; set; } = [];
    }
}
