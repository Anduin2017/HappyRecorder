using Anduin.HappyRecorder.Calendar.Handlers.GetScore;
using Anduin.CommandFramework.Abstracts;
using Anduin.CommandFramework.Framework;

namespace Anduin.HappyRecorder.Calendar
{
    public class CalendarPlugin : IPlugin
    {
        public CommandHandler[] Install()
        {
            return new CommandHandler[]
            {
                new GetCommandHandler()
            };
        }
    }
}
