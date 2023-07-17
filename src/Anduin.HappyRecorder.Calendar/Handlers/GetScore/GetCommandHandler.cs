using Anduin.Framework.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anduin.HappyRecorder.Calendar.Handlers.GetScore;

public class GetCommandHandler : ServiceCommandHandler<GetScoreEntry, Startup>
{
    public override string Name => "get";

    public override string Description => "Get information.";

    public override CommandHandler[] GetSubCommandHandlers()
    {
        return new CommandHandler[]
        {
            new ScoreCommandHandler()
        };
    }
}


public class ScoreCommandHandler : ServiceCommandHandler<GetScoreEntry, Startup>
{
    public override string Name => "score";

    public override string Description => "Get current score for happy.";
}
