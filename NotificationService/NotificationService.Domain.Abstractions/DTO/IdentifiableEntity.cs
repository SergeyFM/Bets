﻿namespace NotificationService.Domain.Abstractions.DTO
{
    /// <summary>
    /// Базовая сущность с идентификатором типа Guid
    /// </summary>
    public abstract class IdentifiableEntity : IEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
