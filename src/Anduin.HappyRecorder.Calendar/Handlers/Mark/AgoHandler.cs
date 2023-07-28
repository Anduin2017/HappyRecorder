using System.CommandLine;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.PluginFramework.Models;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Anduin.HappyRecorder.Calendar.Handlers.Mark;

public class AgoHandler : CommandHandler
{
    public override string Name => "ago";

    public override string Description => "Mark past time as happy.";

    private readonly Option<int> _minutesOption = new(new[] { "-m", "--minutes" }, "How many minutes ago to be marked as happy.")
    {
        IsRequired = true
    };

    public override Option[] GetCommandOptions() => new Option[]
    {
        _minutesOption
    };

    public override void OnCommandBuilt(Command command)
    {
        command.SetHandler(
            Execute,
            CommonOptionsProvider.VerboseOption,
            CommonOptionsProvider.DryRunOption,
            _minutesOption);
    }

    private static async Task Execute(bool verbose, bool dryRun, int minutes)
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
                HappenTime = DateTime.Now.AddMinutes(-1 * minutes)
            });
        }

        await db.SaveChangesAsync();
        logger.LogInformation("Marked {Minutes} ago as happy time", minutes);
    }
}