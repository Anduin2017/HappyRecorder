﻿using System.CommandLine.Invocation;
using Aiursoft.CommandFramework.Framework;
using Aiursoft.CommandFramework.Models;
using Aiursoft.CommandFramework.Services;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Anduin.HappyRecorder.Calendar.Handlers.Get;

public class CalendarHandler : ExecutableCommandHandlerBuilder
{
    protected  override string Name => "calendar";

    protected  override string Description => "Show score calendar.";

    protected override async Task Execute(InvocationContext context)
    {
        var verbose = context.ParseResult.GetValueForOption(CommonOptionsProvider.VerboseOption);
        var services = ServiceBuilder
            .CreateCommandHostBuilder<Startup>(verbose)
            .Build().Services;
        
        var calendar = services.GetRequiredService<CalendarRenderer>();
        await calendar.Render();
    }
}