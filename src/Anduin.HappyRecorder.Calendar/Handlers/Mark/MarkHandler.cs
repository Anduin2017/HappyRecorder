using Aiursoft.CommandFramework.Framework;

namespace Anduin.HappyRecorder.Calendar.Handlers.Mark;

public class MarkHandler : NavigationCommandHandlerBuilder
{
    public override string Name => "mark";

    public override string Description => "Add a new happy record to the database.";

    public override CommandHandlerBuilder[] GetSubCommandHandlers()
    {
        return new CommandHandlerBuilder[]
        {
            new NowHandler(),
            new TimeHandler(),
            new AgoHandler()
        };
    }
}