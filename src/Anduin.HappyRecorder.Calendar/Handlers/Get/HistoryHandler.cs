using System.CommandLine;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.Calendar.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Anduin.HappyRecorder.Calendar.Handlers.Get;

public class HistoryHandler : CommandHandler
{
    public override string Name => "history";

    public override string Description => "Show happy history.";

    private static readonly Option<int> TakeOption = new(
        aliases: new[] { "--take", "-t" },
        description: "History events to take")
    {
        IsRequired = true
    };
    
    public override Option[] GetCommandOptions() => new Option[] { TakeOption };
    
    public override void OnCommandBuilt(Command command)
    {
        command.SetHandler(Execute, 
            CommonOptionsProvider.VerboseOption,
            TakeOption);
    }

    private async Task Execute(bool verbose, int take)
    {
        var services = ServiceBuilder
            .BuildServices<Startup>(verbose)
            .BuildServiceProvider();
        
        var db = services.GetRequiredService<Database>();
        var logger = services.GetRequiredService<ILogger<HistoryHandler>>();
        var items = (await db.GetEvents())
            .OrderByDescending(e => e.HappenTime)
            .Take(take)
            .ToList();

        if (!items.Any())
        {
            logger.LogInformation("No history found.");
        }

        foreach (var item in items)
        {
            logger.LogInformation($"{item.HappenTime.ToLocalTime()}");
        }
    }
}