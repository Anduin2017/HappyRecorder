using Aiursoft.CommandFramework.Framework;

namespace Anduin.HappyRecorder.Calendar.Handlers.Config;

public class ConfigHandler : NavigationCommandHandlerBuilder
{
    public override string Name => "config";

    public override string Description => "Configuration management.";

    public override CommandHandlerBuilder[] GetSubCommandHandlers()
    {
        return new CommandHandlerBuilder[]
        {
            new GetDbLocationHandler(),
            new SetDbLocationHandler()
        };
    }
}