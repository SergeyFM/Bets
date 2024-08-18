namespace NotificationService.Domain.Abstractions.DTO
{
    /// <summary>
    /// Базовая сущность с информацией последней модификации 
    /// </summary>
    public abstract class ModifiableEntity : CreatedEntity, IEntity
    {
        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Кто последний раз изменял
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
