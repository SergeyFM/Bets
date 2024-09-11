namespace Bets.Abstractions.Domain.DTO
{
    /// <summary>
    /// Базовая сущность с информацией о создании
    /// </summary>
    public abstract class CreatedEntity : IdentifiableEntity, IEntity
    {
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Создатель
        /// </summary>
        public string? CreatedBy { get; set; }
    }
}
