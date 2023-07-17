using Anduin.HappyRecorder.Calendar.Handlers.GetScore;
using Anduin.Framework.Abstracts;
using Anduin.Framework.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
