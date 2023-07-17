using Anduin.Framework.Framework;
using Anduin.HappyRecorder.Calendar;
using System.CommandLine;
using System.Reflection;

var descriptionAttribute = (Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;

var program = new RootCommand(descriptionAttribute ?? "Unknown usage.")
    .AddGlobalOptions()
    .AddPlugins(
        new CalendarPlugin()
    );

return await program.InvokeAsync(args);
