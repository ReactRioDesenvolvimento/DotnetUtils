using IdentityModel.Client;

namespace ReactRio.Utils;

public sealed class ClientCredentialsAPI : HttpClient
{
    private readonly Options _options;
    private Token? _token;
    private string? _tokenEndpoint;

    public ClientCredentialsAPI(Options options)
    {
        _options = options;
    }

    public override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = GetAccessTokenAsync(cancellationToken).Result;
        this.SetBearerToken(token);

        return base.Send(request, cancellationToken);
    }

    public override async Task<HttpResponseMessage> SendAsync
    (
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        var token = await GetAccessTokenAsync(cancellationToken);
        this.SetBearerToken(token);

        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken)
    {
        if (_token?.ExpiresOn > DateTime.UtcNow) return _token.AccessToken;

        _tokenEndpoint ??= await GetTokenEndpointAsync(cancellationToken);

        var client = new HttpClient();
        var request = new ClientCredentialsTokenRequest
        {
            Address = _tokenEndpoint,
            ClientId = _options.ClientID,
            ClientSecret = _options.ClientSecret,
            Scope = string.Join(' ', _options.Scopes)
        };

        var response = await client.RequestClientCredentialsTokenAsync(request, cancellationToken);

        _token = new Token(response.AccessToken, DateTime.UtcNow.AddSeconds(response.ExpiresIn));

        return _token.AccessToken;
    }

    private async Task<string> GetTokenEndpointAsync(CancellationToken cancellationToken)
    {
        var client = new HttpClient();
        var disco = await client.GetDiscoveryDocumentAsync(_options.AuthServer.ToString(), cancellationToken);

        return disco.TokenEndpoint;
    }

    public record Options
    (
        Uri AuthServer, string ClientID, string ClientSecret,
        IEnumerable<string> Scopes
    );

    private record Token(string AccessToken, DateTime ExpiresOn);
}
