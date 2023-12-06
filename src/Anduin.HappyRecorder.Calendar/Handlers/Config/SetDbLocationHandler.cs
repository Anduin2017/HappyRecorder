using System.CommandLine;
using System.CommandLine.Invocation;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Anduin.HappyRecorder.Calendar.Handlers.Config;

public class SetDbLocationHandler : ExecutableCommandHandlerBuilder
{
    public override string Name => "set-db-location";
    public override string Description => "Set current program's database file location.";
    
    public override Option[] GetCommandOptions() => new Option[]
    {
        CommonOptionsProvider.PathOptions
    };

    protected override async Task Execute(InvocationContext context)
    {
        var verbose = context.ParseResult.GetValueForOption(CommonOptionsProvider.VerboseOption);
        var path = context.ParseResult.GetValueForOption(CommonOptionsProvider.PathOptions)!;
        
        var services = ServiceBuilder
            .CreateCommandHostBuilder<Startup>(verbose)
            .Build().Services;
        
        var db = services.GetRequiredService<DatabaseManager>();
        await db.SetDbLocatgion(path);
        Console.WriteLine("Set.");
    }
}