using Aiursoft.CommandFramework.Framework;

namespace Anduin.HappyRecorder.Calendar.Handlers.Config;

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