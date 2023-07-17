using Anduin.HappyRecorder.PluginFramework.Services.PluginFramework;
using System.CommandLine;
using System.Reflection;
using Aiursoft.CommandFramework.Extensions;
using Anduin.HappyRecorder.Calendar;

namespace Anduin.HappyRecorder.PluginFramework.Services;

public class Program
{
    public static async Task Main(string[] args)
    {
        var descriptionAttribute = (Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;

        var program = new RootCommand(descriptionAttribute ?? "Unknown usage.")
            .AddGlobalOptions()
            .AddPlugins(
                new CalendarPlugin()
            );

        await program.InvokeAsync(args);
    }
}