using System.CommandLine;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Anduin.HappyRecorder.Calendar.Handlers.Config;

public class SetDbLocationHandler : CommandHandler
{
    public override string Name => "set-db-location";
    public override string Description => "Set current program's database file location.";
    
    public override Option[] GetCommandOptions() => new Option[]
    {
        CommonOptionsProvider.PathOptions
    };

    public override void OnCommandBuilt(Command command)
    {
        command.SetHandler(Execute, 
            CommonOptionsProvider.VerboseOption,
            CommonOptionsProvider.PathOptions);
    }

    private async Task Execute(bool verbose, string path)
    {
        var services = ServiceBuilder
            .BuildHost<Startup>(verbose)
            .Build().Services;
        
        var db = services.GetRequiredService<DatabaseManager>();
        await db.SetDbLocatgion(path);
        Console.WriteLine("Set.");
    }
}