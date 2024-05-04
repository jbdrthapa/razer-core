namespace RazerCore.Common;

public interface IDataAggregator
{
    Task SubscribeAsync();

    Task UnsubscribeAsync();
}