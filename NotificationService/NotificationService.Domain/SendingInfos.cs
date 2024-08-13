using NotificationService.Domain.Abstractions.DTO;
using NotificationService.Domain.Directories;

namespace NotificationService.Domain
{
    /// <summary>
    /// Информация об отправке сообщения конечному пользователю
    /// </summary>
    public sealed class SendingInfos : CreatedEntity
    {
        public SendingStates State { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Идентификатор шаблона сообщения для дополнительного форматирования и/или изменения текста 
        /// </summary>
        public Guid MessageTemplatesId { get; set; }

        /// <summary>
        /// Идентификатор адреса, на который отправляется сообщение
        /// </summary>
        public Guid BettorAddressesId { get; set; }

        /// <summary>
        /// Подробная информация о месте получения сообщения
        /// </summary>
        public BettorAddresses Addresses { get; set; }

        /// <summary>
        /// Номер попытки
        /// </summary>
        public int TrialNumber { get; set; }
    }

    public enum SendingStates
    {
        /// <summary>
        /// Готово к отправке
        /// </summary>
        ReadyToSent = 0,

        /// <summary>
        /// В процессе отправки
        /// </summary>
        InSendingProcess = 1,

        /// <summary>
        /// Сбой
        /// </summary>
        Failure = 2,

        /// <summary>
        /// Сообщение успешно отправлено
        /// </summary>
        Successfully = 3,
    }
}
