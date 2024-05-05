using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RazerCore.Services;

namespace RazerCore.Common;

public class DataAggregator : IDataAggregator
{
    private readonly IHostedService _hostedService;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly ILogger<DataAggregator> _logger;
    private readonly IMessagingService _messagingService;

    public DataAggregator(ILogger<DataAggregator> logger, IHostedService hostedService, IMessagingService messagingService)
    {
        _logger = logger;
        _hostedService = hostedService;
        _cancellationTokenSource = new CancellationTokenSource();
        _messagingService = messagingService;
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

