
namespace BetsService.Models
{
    public sealed class EventOutcomeRequest
    {
        /// <summary>
        /// Описание исхода
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Идентификатор события
        /// </summary>
        public Guid EventId { get; set; }
    }
}
