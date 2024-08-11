namespace NotificationService.Domain.Abstractions.DTO
{
    /// <summary>
    /// Базовая сущность с информацией о создании
    /// </summary>
    public class CreatedEntity : IdentifiableEntity, IEntity
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
