using Aiursoft.CommandFramework.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aiursoft.CommandFramework.Services;

public static class ServiceBuilder
{
    public static ServiceCollection BuildServices<T>(bool verbose) where T : IStartUp, new()
    {
        var services = new ServiceCollection();
        services.AddLogging(logging =>
        {
            logging
                .AddFilter("Microsoft.Extensions", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning);
            logging.AddSimpleConsole(options =>
            {
                options.IncludeScopes = verbose;
                options.SingleLine = true;
                options.TimestampFormat = "mm:ss ";
                options.UseUtcTimestamp = true;
            });

            logging.SetMinimumLevel(verbose ? LogLevel.Trace : LogLevel.Information);
        });

        var startUp = new T();
        startUp.ConfigureServices(services);
        return services;
    }
}
