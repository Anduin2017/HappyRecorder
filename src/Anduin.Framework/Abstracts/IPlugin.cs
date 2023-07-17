using Anduin.Framework.Framework;

namespace Anduin.Framework.Abstracts;

public interface IPlugin
{
    public CommandHandler[] Install();
}
