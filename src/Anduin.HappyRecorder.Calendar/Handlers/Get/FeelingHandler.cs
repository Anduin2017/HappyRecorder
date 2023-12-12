using System.CommandLine.Invocation;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Anduin.HappyRecorder.Calendar.Handlers.Get;

public class FeelingHandler : ExecutableCommandHandlerBuilder
{
    protected  override string Name => "feeling";

    protected  override string Description => "Show current feeling.";

    protected  override string[] Alias => new[] { "feel" };

    protected override async Task Execute(InvocationContext context)
    {
        var verbose = context.ParseResult.GetValueForOption(CommonOptionsProvider.VerboseOption);
        var services = ServiceBuilder
            .CreateCommandHostBuilder<Startup>(verbose)
            .Build().Services;

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