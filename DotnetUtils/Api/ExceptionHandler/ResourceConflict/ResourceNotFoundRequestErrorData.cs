namespace ReactRio.Utils.Api;

public sealed record ResourceConflictRequestErrorData
(
    string Type,
    string ResourceName,
    string ConflictingPropertyName,
    string ConflictingPropertyValue
) : IRequestErrorData;
