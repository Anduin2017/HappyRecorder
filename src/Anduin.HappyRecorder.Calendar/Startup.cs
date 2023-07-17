using Anduin.CommandFramework.Abstracts;
using Anduin.HappyRecorder.Calendar.Handlers.GetScore;
using Microsoft.Extensions.DependencyInjection;

namespace Anduin.HappyRecorder.Calendar;

public class Startup : IStartUp
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<GetScoreEntry>();
    }
}
