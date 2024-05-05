using System.Text.RegularExpressions;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RazerCore.Services;

public class RazerHostedService : IHostedService, IDisposable
{
    private readonly ILogger<RazerHostedService> _logger;
    private readonly IShellService _shellService;
    private readonly object _timerSync;
    private Timer? _timer = null;

    private const int TimerIntervalSecs = 20;

    private bool _disposed = false;

    public RazerHostedService(ILogger<RazerHostedService> logger, IShellService shellService)
    {
        _timerSync = new object();
        _logger = logger;
        _shellService = shellService;
    }

    public string? RazerData { get; private set; }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        lock (_timerSync)
        {
            _logger.LogInformation("Starting Razer hosted service ...");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(TimerIntervalSecs));
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

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
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

        _disposed = true;
    }

    private void DoWork(object? state)
    {
        lock (_timerSync)
        {
            _logger.LogInformation("callback Razer hosted service ");

            ProcessCPUData();
        }
    }

    private void ProcessCPUData()
    {


    }



}