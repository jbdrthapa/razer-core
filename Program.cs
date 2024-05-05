using Microsoft.Extensions.DependencyInjection;
using NATS.Client.Core;
using Microsoft.Extensions.Logging;
using NATS.Client.Hosting;
using RazerCore.Services;
using Microsoft.Extensions.Hosting;
using RazerCore.Common;
using System.Reflection;

namespace RazerCore;

public class RazerCore
{
    public static void Main(string[] args)
    {
        var provider = ConfigureServices();
        var logger = provider.GetService<ILogger<RazerCore>>();

        if (logger == null)
        {
            return;
        }

        var assembly = Assembly.GetEntryAssembly();
        logger.LogInformation($"Starting module {assembly?.GetName().Name} {assembly?.GetName().Version}");

        var dataAggregator = provider.GetService<IDataAggregator>();
        dataAggregator?.SubscribeAsync();

        while (Console.ReadKey().Key != ConsoleKey.Q)
        {
            Thread.Sleep(1000);
        }

        logger.LogInformation($"Stopping module {assembly?.GetName().Name} {assembly?.GetName().Version}");

        dataAggregator?.UnsubscribeAsync();

        logger.LogInformation($"Module {assembly?.GetName().Name} {assembly?.GetName().Version} stopped.");
    }

    private static ServiceProvider ConfigureServices()
    {
        // Add services
        var services = new ServiceCollection()
        .AddLogging(builder => builder.AddConsole())
        .AddHostedService<RazerHostedService>()
        .AddSingleton<IDataAggregator, DataAggregator>()
        .AddSingleton<IMessagingService, MessagingService>()
        .AddNats(1, (opts) => new NatsOpts()
        {
            Url = "127.0.0.1:4222"
        });

        return services.BuildServiceProvider();
    }
}
