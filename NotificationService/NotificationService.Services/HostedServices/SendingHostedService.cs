using HostedService;
using Microsoft.Extensions.Logging;

namespace NotificationService.Services.HostedServices
{
    public class SendingHostedService : HostedServiceBase
    {
        private readonly SendingService _sendingService;
        private readonly ILogger<SendingHostedService> _logger;

        public SendingHostedService(SendingService sendingService
            , ILogger<SendingHostedService> logger)
            : base()
        {
            _sendingService = sendingService;
            _logger = logger;
        }

        protected override TimeSpan Interval => TimeSpan.FromSeconds(20);

        protected override string QueueName => "Sending";

        protected override void LogError(Exception ex, string errorText)
        {
            _logger.LogError(ex, errorText);
        }

        protected override void LogInformation(string infoText, params object[] args)
        {
            _logger.LogInformation(infoText, args);
        }

        protected override async Task OnRunBackgroundWork(CancellationToken ct)
        {
            await _sendingService.SendMessagesAsync();
        }
    }
}
