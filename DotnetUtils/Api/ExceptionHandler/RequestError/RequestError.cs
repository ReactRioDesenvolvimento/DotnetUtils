namespace ReactRio.Utils.Api;

public sealed record RequestErrorData(string Type) : IRequestErrorData;

public class RequestError : BaseRequestError<RequestErrorData>
{
    public RequestError(Exception ex, bool isDevelopment) : base(ex, isDevelopment)
    {
        Code = 500;
        Message = "Um erro inesperado ocorreu";
        Error = new RequestErrorData("unknown-error");
    }
}
