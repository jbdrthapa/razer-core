namespace RazerCore.Services;

public interface IMessagingService : IDisposable
{

    Task PublishAsync(string subject, string data);

    Task SubscribeAsync(string subject);

    Task<string?> RequestAsync(string subject, string data);

}