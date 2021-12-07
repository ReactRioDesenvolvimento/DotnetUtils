using System.Text.Json;

namespace ReactRio.Utils;

public static class Http
{
    public static async Task<TResponse?> Get<TResponse>
    (
        string url, JsonSerializerOptions? options = null, int timeout = 0,
        CancellationToken cancellationToken = default
    ) where TResponse : class
    {
        using var httpClient = new HttpClient();
        try
        {
            if (timeout > 0)
                httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            using var response = await httpClient.GetAsync(url, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var error = response.Content.ReadAsStringAsync(cancellationToken);

                throw new Exception($"HTTP GET faiure: {error}");
            }

            var content = await response.Content.ReadAsStreamAsync(cancellationToken);

            return await JsonSerializer.DeserializeAsync<TResponse>(content, options, cancellationToken);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
