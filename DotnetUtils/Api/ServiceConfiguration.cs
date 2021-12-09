using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReactRio.Utils.Api;

public abstract class ServiceConfiguration
{
    private readonly WebApplicationBuilder _builder;

    protected ServiceConfiguration(WebApplicationBuilder builder)
    {
        _builder = builder;
        Enabled = true;
    }

    protected bool Enabled { get; init; }
    protected bool IsDevEnvironment => _builder.Configuration["ENVIRONMENT"] == "development";

    public void ExecuteServicesConfiguration(IServiceCollection services, ConfigurationManager config)
    {
        if (!Enabled) return;

        ConfigureServices(services, config);
    }

    public void ExecuteAppConfiguration(WebApplication app, ConfigurationManager config)
    {
        if (!Enabled) return;

        ConfigureApp(app, config);
    }

    protected virtual void ConfigureServices(IServiceCollection services, ConfigurationManager config)
    { }

    protected virtual void ConfigureApp(WebApplication app, ConfigurationManager config)
    { }
}
