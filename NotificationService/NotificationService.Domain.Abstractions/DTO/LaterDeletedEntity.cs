namespace NotificationService.Domain.Abstractions.DTO
{
    /// <summary>
    /// Базовая сущность, поддерживающая отложенное удаление
    /// </summary>
    public class LaterDeletedEntity : ModifiableEntity, IEntity
    {
        /// <summary>
        /// Дата удаления
        /// </summary>
        public DateTime? DeletedDate { get; set; }

        /// <summary>
        /// Кем удалено
        /// </summary>
        public string? DeletedBy { get; set; }
    }
}
