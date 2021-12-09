using Microsoft.AspNetCore.Builder;

namespace ReactRio.Utils.Api;

public class App
{
    private readonly WebApplicationBuilder _builder;
    private readonly List<Type> _services;

    public App(WebApplicationBuilder builder)
    {
        _builder = builder;
        _services = new List<Type>();
    }

    public void AddService<T>() where T : ServiceConfiguration
    {
        _services.Add(typeof(T));
    }

    public void Run()
    {
        var services = _services.Select(InitConfiguration).ToList();

        foreach (var serviceConfiguration in services)
            serviceConfiguration.ExecuteServicesConfiguration(_builder.Services, _builder.Configuration);

        var app = _builder.Build();

        foreach (var serviceConfiguration in services)
            serviceConfiguration.ExecuteAppConfiguration(app, _builder.Configuration);

        _services.Clear();
        GC.Collect();

        app.Run();
    }

    private ServiceConfiguration InitConfiguration(Type type)
    {
        var constructor = type.GetConstructors().First();
        var instance = constructor.Invoke(new object[] {_builder});

        return (ServiceConfiguration) instance;
    }
}
