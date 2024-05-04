
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RazerCore.Services;

public class RazerHostedService : IHostedService, IDisposable
{
    private readonly ILogger<RazerHostedService> _logger;

    private readonly object _timerSync;

    private Timer? _timer = null;

    public RazerHostedService(ILogger<RazerHostedService> logger)
    {
        _timerSync = new object();
        _logger = logger;

    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        lock (_timerSync)
        {
            _logger.LogInformation("Starting Razer hosted service ...");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            _logger.LogInformation("Started.");

            return Task.CompletedTask;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        lock (_timerSync)
        {
            _timer?.Change(Timeout.Infinite, 0);
            _logger.LogInformation("Stopped Razer hosted service .");
            return Task.CompletedTask;
        }
    }

    private void DoWork(object? state)
    {
        lock (_timerSync)
        {
            _logger.LogInformation("callback Razer hosted service ");
        }
    }

    public void Dispose()
    {
        lock (_timerSync)
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }
    }


}