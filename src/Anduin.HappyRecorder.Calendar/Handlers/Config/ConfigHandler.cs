using System.CommandLine;
using Anduin.CommandFramework.Framework;
using Anduin.CommandFramework.Models;
using Anduin.CommandFramework.Services;
using Anduin.HappyRecorder.Calendar.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Anduin.HappyRecorder.Calendar.Handlers.Config;

public class GetHandler : CommandHandler
{
    public override string Name => "get";

    public override string Description => "Database result fetcher.";

    public override CommandHandler[] GetSubCommandHandlers()
    {
        return new CommandHandler[]
        {
            new ScoreHandler(),
            new HistoryHandler(),
            new CalendarHandler()
        };
    }
}

public class ScoreHandler : CommandHandler
{
    public override string Name => "score";

    public override string Description => "Show current happiness score.";
}

public class HistoryHandler : CommandHandler
{
    public override string Name => "history";

    public override string Description => "Show happy history.";
}

public class CalendarHandler : CommandHandler
{
    public override string Name => "calendar";

    public override string Description => "Show score calendar.";
}

public class MarkHandler : CommandHandler
{
    public override string Name => "mark";

    public override string Description => "Add a new happy record to the database.";

    public override CommandHandler[] GetSubCommandHandlers()
    {
        return new CommandHandler[]
        {
            new NowHandler(),
            new TimeHandler()
        };
    }
}

public class NowHandler : CommandHandler
{
    public override string Name => "now";

    public override string Description => "Mark current time as happy time.";
}

public class TimeHandler : CommandHandler
{
    public override string Name => "time";

    public override string Description => "Mark specific time as happy time.";
}

public class ConfigHandler : CommandHandler
{
    public override string Name => "config";

    public override string Description => "Configuration management.";

    public override CommandHandler[] GetSubCommandHandlers()
    {
        return new CommandHandler[]
        {
            new GetDbLocationHandler(),
            new SetDbLocationHandler()
        };
    }
}

public class GetDbLocationHandler: CommandHandler
{
    public override string Name => "get-db-location";
    public override string Description => "Get current program's database file location.";

    public override void OnCommandBuilt(Command command)
    {
        command.SetHandler(Execute, CommonOptionsProvider.VerboseOption);
    }

    public async Task Execute(bool verbose)
    {
        var services = ServiceBuilder
            .BuildServices<Startup>(verbose)
            .BuildServiceProvider();
        
        var db = services.GetRequiredService<DatabaseManager>();
        var logger = services.GetRequiredService<ILogger<DatabaseManager>>();

        var dbLocation = await db.GetDbLocation();
        logger.LogInformation("{DbLocation}", dbLocation);
    }
}

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

    public async Task Execute(bool verbose, string path)
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