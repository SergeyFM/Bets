namespace NotificationService.Domain.Abstractions
{
    /// <summary>
    /// Базовая сущность, поддерживающая отложенное удаление
    /// </summary>
    public class BaseLaterDeletedEntity : BaseModifiableEntity
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
