using System.CommandLine.Invocation;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Anduin.HappyRecorder.Calendar.Handlers.Config;

public class GetDbLocationHandler: ExecutableCommandHandlerBuilder
{
    public override string Name => "get-db-location";
    public override string Description => "Get current program's database file location.";

    protected override async Task Execute(InvocationContext context)
    {
        var verbose = context.ParseResult.GetValueForOption(CommonOptionsProvider.VerboseOption);
        
        var services = ServiceBuilder
            .CreateCommandHostBuilder<Startup>(verbose)
            .Build().Services;
        
        var db = services.GetRequiredService<DatabaseManager>();

        var dbLocation = await db.GetDbLocation();
        Console.WriteLine(dbLocation);
    }
}