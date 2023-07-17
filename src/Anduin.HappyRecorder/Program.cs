using Anduin.HappyRecorder.Calendar;
using Anduin.HappyRecorder.PluginFramework;
using System.CommandLine;
using System.Reflection;
using Aiursoft.CommandFramework.Extensions;

namespace Anduin.HappyRecorder
{
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
}