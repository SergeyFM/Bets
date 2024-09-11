
namespace Bets.Abstractions.Domain.Repositories.ModelRequests
{
    public sealed class DeleteRequest
    {
        /// <summary>
        /// Идентификатор удаляемого объекта
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Кто удаляет
        /// </summary>
        public string? DeletedBy { get; set; }
    }
}
