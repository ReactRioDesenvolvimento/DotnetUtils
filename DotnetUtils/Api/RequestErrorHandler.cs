using FluentValidation.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReactRio.Utils.Api;

public class RequestErrorService : ServiceConfiguration
{
    public RequestErrorService(WebApplicationBuilder builder) : base(builder)
    { }

    protected override void ConfigureApp(WebApplication app, ConfigurationManager config)
    {
        app.UseRequestExceptionHandlerMiddleware();
    }

    protected override void ConfigureServices(IServiceCollection services, ConfigurationManager config)
    {
        services.Configure<ApiBehaviorOptions>
        (
            opts =>
            {
                opts.InvalidModelStateResponseFactory = actionContext =>
                {
                    var failures = new List<ValidationFailure>();

                    foreach (var (key, value) in actionContext.ModelState)
                        failures.AddRange
                        (
                            value.Errors.Select
                            (
                                valueError =>
                                    new ValidationFailure(key, valueError.ErrorMessage)
                            )
                        );

                    throw new ValidationFailureException(failures);
                };
            }
        );
    }
}
