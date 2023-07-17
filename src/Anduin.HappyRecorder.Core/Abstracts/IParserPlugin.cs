using Anduin.HappyRecorder.Core.Framework;

namespace Anduin.HappyRecorder.Core.Abstracts;

public interface IParserPlugin
{
    public CommandHandler[] Install();
}
