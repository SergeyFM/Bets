
namespace Bets.Abstractions.Domain.Repositories.ModelRequests
{
    public sealed class DeleteListRequest
    {
        /// <summary>
        /// Идентификаторы сущностей для удаления
        /// </summary>
        public IEnumerable<Guid> Ids { get; set; } = Array.Empty<Guid>();

        /// <summary>
        /// Кто удаляет
        /// </summary>
        public string? DeletedBy { get; set; }
    }
}
