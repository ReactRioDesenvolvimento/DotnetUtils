namespace ReactRio.Utils.Api;

public sealed class ResourceNotFoundRequestError : BaseRequestError<ResourceNotFoundRequestErrorData>
{
    public ResourceNotFoundRequestError(ResourceNotFoundException ex, bool isDevelopment) : base(ex, isDevelopment)
    {
        Code = 404;
        Message = ex.Message;
        Error = new ResourceNotFoundRequestErrorData
        (
            Type: "not-found",
            ResourceName: ex.ResourceName,
            ResourceIdentifierName: ex.ResourceIdentifierName,
            ResourceIdentifier: ex.ResourceIdentifier
        );
    }
}
