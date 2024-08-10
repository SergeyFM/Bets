namespace NotificationService.Domain.Abstractions
{
    /// <summary>
    /// Базовая сущность с информацией последней модификации 
    /// </summary>
    public class BaseModifiableEntity : BaseCreatedEntity
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
