using System.CommandLine;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.PluginFramework.Models;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Anduin.HappyRecorder.Calendar.Handlers.Mark;

public class AgoHandler : ExecutableCommandHandlerBuilder
{
    protected  override string Name => "ago";

    protected  override string Description => "Mark past time as happy.";

    private readonly Option<int> _minutesOption = new(
        name: "--minutes",
        aliases: ["-m"])
    {
        Description = "How many minutes ago to be marked as happy.",
        Required = true
    };

    protected  override Option[] GetCommandOptions() => new Option[]
    {
        _minutesOption
    };

    protected override async Task Execute(ParseResult context)
    {
        var verbose = context.GetValue(CommonOptionsProvider.VerboseOption);
        var dryRun = context.GetValue(CommonOptionsProvider.DryRunOption);
        var minutes = context.GetValue(_minutesOption);
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
                HappenTime = DateTime.Now.AddMinutes(-1 * minutes)
            });
        }

        await db.SaveChangesAsync();
        logger.LogInformation("Marked {Minutes} ago as happy time", minutes);
    }
}
