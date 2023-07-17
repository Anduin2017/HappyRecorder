using Aiursoft.CommandFramework.Abstracts;
using Aiursoft.CommandFramework.Framework;
using Anduin.HappyRecorder.Calendar.Handlers.Config;

namespace Anduin.HappyRecorder.Calendar
{
    public class CalendarPlugin : IPlugin
    {
        public CommandHandler[] Install()
        {
            return new CommandHandler[]
            {
                new GetHandler(),
                new MarkHandler(),
                new ConfigHandler()
            };
        }
    }
}
