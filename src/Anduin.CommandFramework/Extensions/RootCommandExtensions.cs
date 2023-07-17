using Anduin.CommandFramework.Abstracts;
using System.CommandLine;

namespace Anduin.CommandFramework.Extensions;

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
