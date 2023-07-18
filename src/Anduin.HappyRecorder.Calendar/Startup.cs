using Aiursoft.CommandFramework.Abstracts;
using Anduin.HappyRecorder.PluginFramework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Anduin.HappyRecorder.Calendar;

public class Startup : IStartUp
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<Database>();
        services.AddScoped<DatabaseManager>();
        services.AddScoped<CalendarRenderer>();
        services.AddScoped<Algorithm>();
    }
}
