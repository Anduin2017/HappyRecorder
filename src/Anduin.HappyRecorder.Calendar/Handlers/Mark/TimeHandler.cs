using System.CommandLine;
using System.CommandLine.Invocation;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.PluginFramework.Models;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Anduin.HappyRecorder.Calendar.Handlers.Mark;

public class TimeHandler : ExecutableCommandHandlerBuilder
{
    protected  override string Name => "time";

    protected  override string Description => "Mark specific time as happy time.";
    
    private readonly Option<DateTime> _timeOption = new(new[] { "-t", "--time" }, "Time string about when you triggered.")
    {
        IsRequired = true
    };

    protected  override Option[] GetCommandOptions() => new Option[]
    {
        _timeOption
    };

    protected override async Task Execute(ParseResult context)
    {
        var verbose = context.GetValue(CommonOptionsProvider.VerboseOption);
        var dryRun = context.GetValue(CommonOptionsProvider.DryRunOption);
        var time = context.GetValue(_timeOption);
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
                HappenTime = time
            });
        }

        await db.SaveChangesAsync();
        logger.LogInformation("Time {Time} marked as happy time", time);
    }
}