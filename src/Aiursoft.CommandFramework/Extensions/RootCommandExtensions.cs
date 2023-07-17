using System.CommandLine;
using Aiursoft.CommandFramework.Abstracts;

namespace Aiursoft.CommandFramework.Extensions;

public static class RootCommandExtensions
{
    public static RootCommand AddPlugins(this RootCommand command, params IPlugin[] pluginInstallers)
    {
        foreach (var plugin in pluginInstallers)
        {
            foreach (var pluginFeature in plugin.Install())
            {
                command.Add(pluginFeature.BuildAsCommand());
            }
        }
        return command;
    }
}
