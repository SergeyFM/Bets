
namespace BetsService.Models
{
    public sealed class EventOutcomeUpdateRequest
    {
        /// <summary>
        /// Идентификатор исхода события
        /// </summary>
        public Guid Id { get; set; }

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
        /// Кто последний раз изменял
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
