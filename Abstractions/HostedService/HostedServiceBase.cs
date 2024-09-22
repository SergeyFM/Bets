using Microsoft.Extensions.Hosting;
using System.Timers;
using Timer = System.Timers.Timer;

namespace HostedService
{
    public abstract class HostedServiceBase : IHostedService, IDisposable
    {
        private readonly Timer _timer;
        private readonly CancellationTokenSource _cts;

        private volatile bool _running;

        protected abstract string QueueName { get; }
        protected virtual TimeSpan Interval => TimeSpan.FromDays(1);
        protected bool IsCancellationRequested => _cts.Token.IsCancellationRequested;

        public HostedServiceBase()
        {
            _cts = new CancellationTokenSource();

            _timer = new Timer
            {
                AutoReset = true,
                Interval = Interval.TotalMilliseconds
            };

            _timer.Elapsed += OnTimerElapsed;
        }

        public Task StartAsync(CancellationToken ct) => Task.Run(() => { _timer.Start(); }, ct);
        public Task StopAsync(CancellationToken ct) => Task.Run(() => { _cts.Cancel(); _timer.Stop(); }, ct);

        protected abstract void LogInformation(string infoText, params object[] args);
        protected abstract void LogError(Exception ex, string errorText);

        protected string Wrap(string value) => string.Format("Очередь {0}: {1}", QueueName, value);

        async void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_running || IsCancellationRequested)
            {
                return;
            }

            _running = true;

            try
            {
                LogInformation(Wrap("Начало обработки"));

                await OnRunBackgroundWork(_cts.Token);

                LogInformation(Wrap("Конец обработки"));
            }
            catch (Exception ex)
            {
                LogError(ex, Wrap("Ошибка обработки"));
            }

            _running = false;
        }

        protected virtual Task OnRunBackgroundWork(CancellationToken ct) => Task.CompletedTask;

        public void Dispose()
        {
            _timer.Elapsed -= OnTimerElapsed;
            _timer.Dispose();
            _cts.Dispose();
        }
    }
}
