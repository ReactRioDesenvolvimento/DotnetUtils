namespace ReactRio.Utils.Api;

public interface IRequestError
{
    public int Code { get; set; }
    public string Message { get; set; }
}
