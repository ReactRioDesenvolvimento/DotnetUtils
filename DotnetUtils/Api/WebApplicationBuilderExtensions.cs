using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace ReactRio.Utils.Api;

public static class WebApplicationBuilderExtensions
{
    public static bool IsDevEnvironment(this WebApplicationBuilder builder)
    {
        return builder.Environment.IsDevelopment();
    }
}
