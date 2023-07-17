using Aiursoft.CommandFramework.Framework;

namespace Anduin.HappyRecorder.Calendar.Handlers.Get;

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