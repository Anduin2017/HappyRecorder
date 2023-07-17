using Aiursoft.CommandFramework.Framework;

namespace Anduin.HappyRecorder.Calendar.Handlers.Get;

public class ScoreHandler : CommandHandler
{
    public override string Name => "score";

    public override string Description => "Show current happiness score.";
}