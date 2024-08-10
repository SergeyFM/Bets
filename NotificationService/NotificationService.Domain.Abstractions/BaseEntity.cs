namespace NotificationService.Domain.Abstractions
{
    /// <summary>
    /// Базовая сущность с идентификатором типа Guid
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
