namespace ReactRio.Utils.Api;

public static class BaseRequestErrorMapper
{
    public static IRequestError FromException
        (Exception ex, bool isDevelopment, Dictionary<Type, Func<Exception, bool, IRequestError>> additionalMaps)
    {
        var exType = ex.GetType();

        if (additionalMaps.ContainsKey(exType)) return additionalMaps[exType].Invoke(ex, isDevelopment);

        return ex switch
        {
            ResourceNotFoundException e => new ResourceNotFoundRequestError(e, isDevelopment),
            ResourceConflictException e => new ResourceConflictRequestError(e, isDevelopment),
            ValidationFailureException e => new ValidationFailureRequestError(e, isDevelopment),
            InvalidRequestException e => new InvalidRequestError(e, isDevelopment),
            _ => new RequestError(ex, isDevelopment)
        };
    }
}
