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
    
    private string GetFeeling(double score)
    {
        if (score >= 90)
            return "ideal";
        else if (score >= 80)
            return "excellent";
        else if (score >= 70)
            return "awesome";
        else if (score >= 60)
            return "good";
        else if (score >= 50)
            return "acceptable";
        else if (score >= 40)
            return "tired";
        else if (score >= 30)
            return "very hard to accept";
        else if (score >= 20)
            return "bad";
        else if (score >= 10)
            return "shit";
        else if (score >= 0)
            return "about to die...";
        else
            throw new InvalidOperationException();
    }
}