namespace NotificationService.Domain.Abstractions
{
    /// <summary>
    /// Базовая сущность с информацией о создании
    /// </summary>
    public class BaseCreatedEntity : BaseEntity
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
