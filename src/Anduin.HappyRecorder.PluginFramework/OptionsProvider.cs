using Anduin.CommandFramework.Models;
using System;
using System.CommandLine;

namespace Anduin.HappyRecorder.PluginFramework;

public static class OptionsProvider
{
    public static readonly Option<bool> DryRunOption = CommonOptionsProvider.DryRunOption;
    public static readonly Option<bool> VerboseOption = CommonOptionsProvider.VerboseOption;

    private static Option[] GetGlobalOptions()
    {
        return new Option[]
        {
                DryRunOption,
                VerboseOption
        };
    }

    public static RootCommand AddGlobalOptions(this RootCommand command)
    {
        var options = GetGlobalOptions();
        foreach (var option in options)
        {
            command.AddGlobalOption(option);
        }
        return command;
    }
}
