using System.CommandLine;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Anduin.HappyRecorder.Calendar.Handlers.Config;

public class SetDbLocationHandler : ExecutableCommandHandlerBuilder
{
    protected  override string Name => "set-db-location";
    protected  override string Description => "Set current program's database file location.";
    
    protected  override Option[] GetCommandOptions() => new Option[]
    {
        CommonOptionsProvider.PathOptions
    };

    protected override async Task Execute(ParseResult context)
    {
        var verbose = context.GetValue(CommonOptionsProvider.VerboseOption);
        var path = context.GetValue(CommonOptionsProvider.PathOptions)!;
        
        var services = ServiceBuilder
            .CreateCommandHostBuilder<Startup>(verbose)
            .Build().Services;
        
        var db = services.GetRequiredService<DatabaseManager>();
        await db.SetDbLocatgion(path);
        Console.WriteLine("Set.");
    }
}