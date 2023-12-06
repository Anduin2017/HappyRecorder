using Aiursoft.CommandFramework.Framework;

namespace Anduin.HappyRecorder.Calendar.Handlers.Get;

public class GetHandler : NavigationCommandHandlerBuilder
{
    public override string Name => "get";

    public override string Description => "Database result fetcher.";

    public override CommandHandlerBuilder[] GetSubCommandHandlers()
    {
        return new CommandHandlerBuilder[]
        {
            new ScoreHandler(),
            new HistoryHandler(),
            new CalendarHandler(),
            new FeelingHandler()
        };
    }
}