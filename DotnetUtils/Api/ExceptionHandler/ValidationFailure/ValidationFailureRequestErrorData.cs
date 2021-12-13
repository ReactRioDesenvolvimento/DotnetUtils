namespace ReactRio.Utils.Api;

public sealed record ValidationFailureRequestErrorData
(
    string Type,
    List<ValidationFailureItem> Failures
) : IRequestErrorData;

public sealed record ValidationFailureItem(string Field, string Error);
