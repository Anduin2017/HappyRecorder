using Aiursoft.CommandFramework.Models;
using System.CommandLine;

namespace Anduin.HappyRecorder.PluginFramework.Services.PluginFramework;

public static class OptionsProvider
{
    public static RootCommand AddGlobalOptions(this RootCommand command)
    {
        var options = new Option[]
        {
            CommonOptionsProvider.DryRunOption,
            CommonOptionsProvider.VerboseOption
        };
        foreach (var option in options)
        {
            command.AddGlobalOption(option);
        }
        return command;
    }
}
