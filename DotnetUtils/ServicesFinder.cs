using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ReactRio.Utils;

public interface IService
{ }

public interface ITransientService : IService
{ }

public interface IScopedService : IService
{ }

public interface ISingletonService : IService
{ }

public static class ServicesFinder
{
    public static void FindAndConfigureServices(this IServiceCollection serviceCollection, Assembly assembly)
    {
        var services = assembly.ExportedTypes.Where(t => t.IsAssignableTo(typeof(IService))).ToList();

        foreach (var service in services)
            if (service.IsAssignableTo(typeof(ISingletonService)))
                serviceCollection.AddSingleton(service);
            else if (service.IsAssignableTo(typeof(IScopedService)))
                serviceCollection.AddScoped(service);
            else if (service.IsAssignableTo(typeof(ITransientService))) serviceCollection.AddTransient(service);
    }
}
