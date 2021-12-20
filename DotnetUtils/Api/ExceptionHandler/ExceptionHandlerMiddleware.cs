using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sentry;

namespace ReactRio.Utils.Api;

public class ExceptionHandlerMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;
    private readonly Dictionary<Type, Func<Exception, bool, IRequestError>> AdditionalErrorMaps;

    public ExceptionHandlerMiddleware
    (
        Dictionary<Type, Func<Exception, bool, IRequestError>> additionalErrorMaps, RequestDelegate next,
        ILogger<ExceptionHandlerMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
        AdditionalErrorMaps = additionalErrorMaps;
    }

    public async Task InvokeAsync(HttpContext context, IWebHostEnvironment env, IConfiguration config)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Requeste exception");
            var isDevEnvironment = env.IsDevelopment() || config["EnableDevSettings"] == "true";

            var error = BaseRequestErrorMapper.FromException(e, isDevEnvironment, AdditionalErrorMaps);

            SentrySdk.CaptureException(e);

            context.Response.StatusCode = error.Code;
            await context.Response.WriteAsJsonAsync((object) error);
        }
    }
}

public static class RequestExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestExceptionHandlerMiddleware
    (
        this IApplicationBuilder app, Dictionary<Type, Func<Exception, bool, IRequestError>>? additionalErrorMaps = null
    )
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>
            (additionalErrorMaps ?? new Dictionary<Type, Func<Exception, bool, IRequestError>>());

        return app;
    }
}
