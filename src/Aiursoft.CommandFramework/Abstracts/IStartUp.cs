using Microsoft.Extensions.DependencyInjection;

namespace Aiursoft.CommandFramework.Abstracts;

public interface IStartUp
{
    public void ConfigureServices(IServiceCollection services);
}
