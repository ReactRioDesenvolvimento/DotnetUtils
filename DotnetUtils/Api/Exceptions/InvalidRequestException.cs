namespace ReactRio.Utils.Api;

public sealed class InvalidRequestException : RequestException
{
    public InvalidRequestException(string message) : base(message)
    { }
}
