using FluentValidation.Results;

namespace ReactRio.Utils.Api;

public class ValidationFailureException : RequestException
{
    public readonly IEnumerable<ValidationFailure> ValidationFailures;

    public ValidationFailureException
        (IEnumerable<ValidationFailure> failures) : base("A API detectou uma inconsistência com os dados")
    {
        ValidationFailures = failures;
    }
}
