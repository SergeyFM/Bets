using Bets.Abstractions.Domain.DTO;

namespace NotificationService.Domain.Directories
{
    /// <summary>
    /// Справочник источников сообщений
    /// </summary>
    public sealed class MessageSources : LaterDeletedEntity
    {
        // Вместо него просто базовый Id
        ///// <summary>
        ///// Идентификатор сервиса источника
        ///// </summary>
        //public Guid AppId { get; set; }

        /// <summary>
        /// Описание источника
        /// </summary>
        public string? Description { get; set; }
    }
}
