using Anduin.HappyRecorder.Core.Framework;
using Anduin.HappyRecorder.FFmpeg;
using System.CommandLine;
using System.Reflection;

var descriptionAttribute = (Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;

var program = new RootCommand(descriptionAttribute ?? "Unknown usage.")
    .AddGlobalOptions()
    .AddPlugins(
        new FFmpegPlugin()
    );

return await program.InvokeAsync(args);
