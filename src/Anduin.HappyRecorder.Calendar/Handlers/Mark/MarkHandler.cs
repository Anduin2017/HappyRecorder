using Aiursoft.CommandFramework.Framework;

namespace Anduin.HappyRecorder.Calendar.Handlers.Mark;

public class MarkHandler : NavigationCommandHandlerBuilder
{
    protected  override string Name => "mark";

    protected  override string Description => "Add a new happy record to the database.";

    protected override CommandHandlerBuilder[] GetSubCommandHandlers()
    {
        return
        [
            new NowHandler(),
            new TimeHandler(),
            new AgoHandler()
        ];
    }
}