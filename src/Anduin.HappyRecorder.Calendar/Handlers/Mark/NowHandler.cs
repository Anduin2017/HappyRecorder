using System.CommandLine.Invocation;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.PluginFramework.Models;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Anduin.HappyRecorder.Calendar.Handlers.Mark;

public class NowHandler : ExecutableCommandHandlerBuilder
{
    protected  override string Name => "now";

    protected  override string Description => "Mark current time as happy time.";

    protected override async Task Execute(ParseResult context)
    {
        var verbose = context.GetValue(CommonOptionsProvider.VerboseOption);
        var dryRun = context.GetValue(CommonOptionsProvider.DryRunOption);
        var services = ServiceBuilder
            .CreateCommandHostBuilder<Startup>(verbose)
            .Build().Services;

        var db = services.GetRequiredService<Database>();
        var logger = services.GetRequiredService<ILogger<NowHandler>>();
        var events = await db.GetEvents();

        if (!dryRun)
        {
            logger.LogInformation("Adding new event");
            events.Add(new Event
            {
                HappenTime = DateTime.Now
            });
        }
        await db.SaveChangesAsync();
        logger.LogInformation("Marked current time as happy time");
    }
}