namespace ReactRio.Utils.Api;

public class UnauthorizedException : RequestException
{
    public UnauthorizedException() : base("Operação não autorizada")
    { }

    public UnauthorizedException(string message) : base(message)
    { }
}
