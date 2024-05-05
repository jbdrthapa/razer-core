using Microsoft.Extensions.Logging;

using NATS.Client.Core;

namespace RazerCore.Services;

public class MessagingService : IMessagingService
{
    private readonly ILogger<MessagingService> _logger;
    private readonly INatsConnection _natsConnection;
    private bool _disposed = false;

    private const int RequestTimeout = 3;

    public MessagingService(ILogger<MessagingService> logger, INatsConnection natsConnection)
    {
        _logger = logger;
        _natsConnection = natsConnection;
    }

    public async Task PublishAsync(string subject, string data)
    {
        await _natsConnection.PublishAsync(subject, data);
    }

    public async Task<string?> RequestAsync(string subject, string data)
    {
        var replyOpts = new NatsSubOpts { Timeout = TimeSpan.FromSeconds(RequestTimeout) };

        var reply = await _natsConnection.RequestAsync<string, string>(subject, data, replyOpts: replyOpts);

        return reply.Data;
    }

    public async Task SubscribeAsync(string subject)
    {
        await _natsConnection.SubscribeCoreAsync<string>(subject);
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

            // Free any other managed objects here.
            //
        }

        _disposed = true;
    }
}