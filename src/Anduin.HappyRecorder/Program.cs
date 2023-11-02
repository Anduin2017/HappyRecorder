using Anduin.HappyRecorder.PluginFramework.Services.PluginFramework;
using Aiursoft.CommandFramework;
using Aiursoft.CommandFramework.Extensions;
using Anduin.HappyRecorder.Calendar;

return await new AiursoftCommand()
    .Configure(command =>
    {
        command
            .AddGlobalOptions()
            .AddPlugins(
                new CalendarPlugin()
            );
    })
    .RunAsync(args);