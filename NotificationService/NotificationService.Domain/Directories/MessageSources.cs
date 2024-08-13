﻿using NotificationService.Domain.Abstractions.DTO;

namespace NotificationService.Domain.Directories
{
    /// <summary>
    /// Справочник источников сообщений
    /// </summary>
    public sealed class MessageSources : LaterDeletedEntity
    {
        /// <summary>
        /// Идентификатор сервиса источника
        /// </summary>
        public Guid AppId { get; set; }

        /// <summary>
        /// Описание источника
        /// </summary>
        public string? Description { get; set; }
    }
}
