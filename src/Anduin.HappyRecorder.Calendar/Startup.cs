using Anduin.CommandFramework.Abstracts;
using Anduin.HappyRecorder.Calendar.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Anduin.HappyRecorder.Calendar;

public class Startup : IStartUp
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<DatabaseManager>();
    }
}
