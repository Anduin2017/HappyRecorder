using Aiursoft.CommandFramework.Framework;

namespace Anduin.HappyRecorder.Calendar.Handlers.Mark;

public class MarkHandler : NavigationCommandHandlerBuilder
{
    protected  override string Name => "mark";

    protected  override string Description => "Add a new happy record to the database.";

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