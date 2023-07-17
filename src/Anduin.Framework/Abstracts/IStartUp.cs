using Microsoft.Extensions.DependencyInjection;

namespace Anduin.Framework.Abstracts;

public interface IStartUp
{
    public void ConfigureServices(IServiceCollection services);
}
