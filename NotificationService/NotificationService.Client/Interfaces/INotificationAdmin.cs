using NotificationService.Models;
using NotificationService.Models.Common;

namespace NotificationService.Client.Interfaces
{
    /// <summary>
    /// Содержит методы для администрирования сервиса уведомлений
    /// </summary>
    public interface INotificationAdmin
    {
        Task<CreateResponse> AddBettorAddressesAsync(BettorAddressesRequest request, CancellationToken ct);
    }
}
