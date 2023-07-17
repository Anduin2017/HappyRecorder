using Anduin.CommandFramework.Framework;

namespace Anduin.CommandFramework.Abstracts;

public interface IPlugin
{
    public CommandHandler[] Install();
}
