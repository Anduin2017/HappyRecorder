using System.CommandLine;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.PluginFramework.Models;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Anduin.HappyRecorder.Calendar.Handlers.Mark;

public class TimeHandler : CommandHandler
{
    public override string Name => "time";

    public override string Description => "Mark specific time as happy time.";
    
    private readonly Option<DateTime> _timeOption = new(new[] { "-t", "--time" }, "Time string about when you triggered.")
    {
        IsRequired = true
    };

    public override Option[] GetCommandOptions() => new Option[]
    {
        _timeOption
    };

    public override void OnCommandBuilt(Command command)
    {
        command.SetHandler(
            Execute,
            CommonOptionsProvider.VerboseOption,
            CommonOptionsProvider.DryRunOption,
            _timeOption);
    }

    private static async Task Execute(bool verbose, bool dryRun, DateTime time)
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
                HappenTime = time
            });
        }

        await db.SaveChangesAsync();
        logger.LogInformation("Time {Time} marked as happy time", time);
    }
}