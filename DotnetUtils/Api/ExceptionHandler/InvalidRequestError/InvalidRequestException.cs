namespace ReactRio.Utils.Api;

public sealed record InvalidRequestErroData(string Type) : IRequestErrorData;

public class InvalidRequestError : BaseRequestError<InvalidRequestErroData>
{
    public InvalidRequestError(InvalidRequestException ex, bool isDevelopment) : base(ex, isDevelopment)
    {
        Code = 400;
        Message = ex.Message;
        Error = new InvalidRequestErroData("invalid-operation");
    }
}
