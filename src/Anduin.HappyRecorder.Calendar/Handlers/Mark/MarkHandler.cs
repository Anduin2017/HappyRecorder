using Aiursoft.CommandFramework.Framework;

namespace Anduin.HappyRecorder.Calendar.Handlers.Mark;

public class MarkHandler : CommandHandler
{
    public override string Name => "mark";

    public override string Description => "Add a new happy record to the database.";

    public override CommandHandler[] GetSubCommandHandlers()
    {
        return new CommandHandler[]
        {
            new NowHandler(),
            new TimeHandler(),
            new AgoHandler()
        };
    }
}