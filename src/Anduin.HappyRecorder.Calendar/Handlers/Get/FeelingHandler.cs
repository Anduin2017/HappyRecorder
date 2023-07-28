using System.CommandLine;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Anduin.HappyRecorder.Calendar.Handlers.Get;

public class FeelingHandler : CommandHandler
{
    public override string Name => "feeling";

    public override string Description => "Show current feeling.";

    public override string[] Alias => new[] { "feel" };

    public override void OnCommandBuilt(Command command)
    {
        command.SetHandler(Execute, CommonOptionsProvider.VerboseOption);
    }

    private async Task Execute(bool verbose)
    {
        var services = ServiceBuilder
            .BuildServices<Startup>(verbose)
            .BuildServiceProvider();

        var algorithm = services.GetRequiredService<Algorithm>();
        var score = await algorithm.GetPoints(false, DateTime.Now);
        var feeling = GetFeeling(score);
        Console.WriteLine(feeling);
    }
    
    private static string GetFeeling(double score)
    {
        return score switch
        {
            >= 90 => "ideal",
            >= 80 => "excellent",
            >= 70 => "awesome",
            >= 60 => "good",
            >= 50 => "acceptable",
            >= 40 => "tired",
            >= 30 => "very hard to accept",
            >= 20 => "bad",
            >= 10 => "shit",
            >= 0 => "about to die...",
            _ => throw new InvalidOperationException()
        };
    }
}