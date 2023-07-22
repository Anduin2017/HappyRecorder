using System.CommandLine;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Anduin.HappyRecorder.Calendar.Handlers.Get;

public class CalendarHandler : CommandHandler
{
    public override string Name => "calendar";

    public override string Description => "Show score calendar.";

    public override void OnCommandBuilt(Command command)
    {
        command.SetHandler(Execute, CommonOptionsProvider.VerboseOption);
    }

    private async Task Execute(bool verbose)
    {
        var services = ServiceBuilder
            .BuildServices<Startup>(verbose)
            .BuildServiceProvider();
        
        var calendar = services.GetRequiredService<CalendarRenderer>();
        await calendar.Render();
    }
}