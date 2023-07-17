using Aiursoft.CommandFramework.Framework;

namespace Aiursoft.CommandFramework.Abstracts;

public interface IPlugin
{
    public CommandHandler[] Install();
}
