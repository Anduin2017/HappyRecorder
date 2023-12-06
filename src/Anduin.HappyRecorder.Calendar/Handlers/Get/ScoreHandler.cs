using System.CommandLine.Invocation;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Anduin.HappyRecorder.Calendar.Handlers.Get;

public class ScoreHandler : ExecutableCommandHandlerBuilder
{
    public override string Name => "score";

    public override string Description => "Show current happiness score.";
    
    protected override async Task Execute(InvocationContext context)
    {
        var verbose = context.ParseResult.GetValueForOption(CommonOptionsProvider.VerboseOption);
        var services = ServiceBuilder
            .CreateCommandHostBuilder<Startup>(verbose)
            .Build().Services;
        
        var algorithm = services.GetRequiredService<Algorithm>();
        var score = await algorithm.GetPoints(false, DateTime.Now);
        Console.WriteLine($"{score:0.00}");
    }
}