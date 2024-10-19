using Bets.Abstractions.Domain.DTO;

namespace BetsService.Domain
{
    /// <summary>
    /// Исходы событий
    /// </summary>
    public class EventOutcomes : LaterDeletedEntity
    {
        /// <summary>
        /// Описание исхода
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Коэф., на который надо домножить ставку для расчета выигрыша
        /// </summary>
        public decimal CurrentOdd { get; set; }

        /// <summary>
        /// Верно, если исход завершился успехом
        /// </summary>
        public bool? IsHappened { get; set; }

        /// <summary>
        /// Верно, если событие закрыто или отменено
        /// </summary>
        public bool BetsClosed { get; set; }

        /// <summary>
        /// Идентификатор события
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// Событие, для которого возможен этот исход
        /// </summary>
        public required virtual Events Event { get; set; }
    }
}
