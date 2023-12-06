using Aiursoft.CommandFramework.Abstracts;
using Anduin.HappyRecorder.Calendar.Handlers.Config;
using Anduin.HappyRecorder.Calendar.Handlers.Get;
using Anduin.HappyRecorder.Calendar.Handlers.Mark;

namespace Anduin.HappyRecorder.Calendar;

public class CalendarPlugin : IPlugin
{
    public ICommandHandlerBuilder[] Install()
    {
        return new ICommandHandlerBuilder[]
        {
            new GetHandler(),
            new MarkHandler(),
            new ConfigHandler()
        };
    }
}