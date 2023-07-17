using Microsoft.Extensions.DependencyInjection;

namespace Anduin.HappyRecorder.Core.Abstracts;

public interface IStartUp
{
    public void ConfigureServices(IServiceCollection services);
}
