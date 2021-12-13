namespace ReactRio.Utils.Api;

public sealed class ResourceConflictRequestError : BaseRequestError<ResourceConflictRequestErrorData>
{
    public ResourceConflictRequestError(ResourceConflictException ex, bool isDevelopment) : base(ex, isDevelopment)
    {
        Code = 409;
        Message = ex.Message;
        Error = new ResourceConflictRequestErrorData
        (
            Type: "conflict",
            ResourceName: ex.ResourceName,
            ConflictingPropertyName: ex.ConflictingPropertyName,
            ConflictingPropertyValue: ex.ConflictingPropertyValue
        );
    }
}
