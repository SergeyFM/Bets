
namespace NotificationService.Models.Enums
{
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
