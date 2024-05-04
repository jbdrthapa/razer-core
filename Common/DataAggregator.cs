using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RazerCore.Services;

namespace RazerCore.Common;

public class DataAggregator : IDataAggregator
{
    private readonly IHostedService _hostedService;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly ILogger<DataAggregator> _logger;

    public DataAggregator(ILogger<DataAggregator> logger, IHostedService hostedService)
    {
        _logger = logger;
        _hostedService = hostedService;
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public async Task SubscribeAsync()
    {
        await _hostedService.StartAsync(_cancellationTokenSource.Token);
    }

    public async Task UnsubscribeAsync()
    {
        await _hostedService.StopAsync(_cancellationTokenSource.Token);
    }

}

