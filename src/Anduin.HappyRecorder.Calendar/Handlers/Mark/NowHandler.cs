using System.CommandLine;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.PluginFramework.Models;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Anduin.HappyRecorder.Calendar.Handlers.Mark;

public class NowHandler : CommandHandler
{
    public override string Name => "now";

    public override string Description => "Mark current time as happy time.";

    public override void OnCommandBuilt(Command command)
    {
        command.SetHandler(
            Execute, 
            CommonOptionsProvider.VerboseOption, 
            CommonOptionsProvider.DryRunOption);
    }

    private async Task Execute(bool verbose, bool dryRun)
    {
        var services = ServiceBuilder
            .BuildServices<Startup>(verbose)
            .BuildServiceProvider();

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