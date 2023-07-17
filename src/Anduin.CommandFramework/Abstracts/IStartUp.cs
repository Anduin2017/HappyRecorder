using Microsoft.Extensions.DependencyInjection;

namespace Anduin.CommandFramework.Abstracts;

public interface IStartUp
{
    public void ConfigureServices(IServiceCollection services);
}
