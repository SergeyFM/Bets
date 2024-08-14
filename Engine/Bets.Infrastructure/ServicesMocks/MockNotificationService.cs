using System.Threading.Tasks;
using Bets.Application.Services;

namespace Bets.Infrastructure.ServicesMocks;

public class MockNotificationService : INotificationService {
    public Task<bool> SendEmailAsync(string email, string subject, string body) =>
        // Always return true, simulating a successful email send.
        Task.FromResult(true);
}
