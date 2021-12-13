namespace ReactRio.Utils.Api;

public sealed class ValidationFailureRequestError : BaseRequestError<ValidationFailureRequestErrorData>
{
    public ValidationFailureRequestError(ValidationFailureException ex, bool isDevelopment) : base(ex, isDevelopment)
    {
        Code = 400;
        Message = ex.Message;
        Error = new ValidationFailureRequestErrorData
        (
            Type: "validation-error",
            Failures: ex.ValidationFailures.Select
                (f => new ValidationFailureItem(f.PropertyName, f.ErrorMessage)).ToList()
        );
        ;
    }
}
