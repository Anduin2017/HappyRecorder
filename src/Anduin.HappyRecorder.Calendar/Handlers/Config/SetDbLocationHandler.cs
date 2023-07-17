using System.CommandLine;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            .BuildServices<Startup>(verbose)
            .BuildServiceProvider();
        
        var db = services.GetRequiredService<DatabaseManager>();
        var logger = services.GetRequiredService<ILogger<DatabaseManager>>();

        await db.SetDbLocatgion(path);
        logger.LogInformation("Success");
    }
}