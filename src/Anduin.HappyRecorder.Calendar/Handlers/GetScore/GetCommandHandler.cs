using Anduin.CommandFramework.Framework;
using Anduin.CommandFramework.Models;
using Anduin.CommandFramework.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anduin.HappyRecorder.Calendar.Handlers.GetScore;

public class GetCommandHandler : CommandHandler
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

public class ScoreCommandHandler : CommandHandler
{
    public override string Name => "score";

    public override string Description => "Get current score for happy.";

    public override void OnCommandBuilt(Command command)
    {
        command.SetHandler(
            Execute,
            CommonOptionsProvider.DryRunOption,
            CommonOptionsProvider.VerboseOption);
    }

    public Task Execute(bool dryRun, bool verbose)
    {
        return ServiceBuilder
            .BuildServices<Startup>(verbose)
            .BuildServiceProvider()
            .GetRequiredService<GetScoreEntry>()
            .OnServiceStartedAsync();
    }
}
