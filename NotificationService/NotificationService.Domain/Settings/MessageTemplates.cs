using NotificationService.Domain.Abstractions.DTO;
using NotificationService.Domain.Directories;

namespace NotificationService.Domain.Settings
{
    /// <summary>
    /// Шаблоны сообщений
    /// </summary>
    public sealed class MessageTemplates : LaterDeletedEntity
    {
        /// <summary>
        /// Путь к файлу с шаблоном (обычно html разметка)
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Если ложь, то шаблон временно не используется
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Идентификатор источника сообщения, которому соответствует шаблон
        /// </summary>
        public Guid MessageSourcesId { get; set; }

        /// <summary>
        /// Источник сообщения, которому соответствует шаблон
        /// </summary>
        public MessageSources messageSource {  get; set; }

        /// <summary>
        /// Идентификатор мессенджера, для которого следует применять шаблон
        /// </summary>
        public Guid MessengersId { get; set; }

        /// <summary>
        /// Мессенджер, для которого следует применять шаблон
        /// </summary>
        public Messengers messenger { get; set; }
    }
}
