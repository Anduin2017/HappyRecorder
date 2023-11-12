using System.CommandLine;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Anduin.HappyRecorder.Calendar.Handlers.Get;

public class HistoryHandler : CommandHandler
{
    public override string Name => "history";

    public override string Description => "Show happy history.";

    public override void OnCommandBuilt(Command command)
    {
        command.SetHandler(Execute, CommonOptionsProvider.VerboseOption);
    }

    private async Task Execute(bool verbose)
    {
        var services = ServiceBuilder
            .CreateCommandHostBuilder<Startup>(verbose)
            .Build().Services;
        
        var algorithm = services.GetRequiredService<Algorithm>();
        await algorithm.GetPoints(true, DateTime.Now);
    }
}