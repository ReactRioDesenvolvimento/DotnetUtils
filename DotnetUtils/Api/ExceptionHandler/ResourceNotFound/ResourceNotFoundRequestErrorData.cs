namespace ReactRio.Utils.Api;

public sealed record ResourceNotFoundRequestErrorData
(
    string Type,
    string ResourceName,
    string ResourceIdentifierName,
    string ResourceIdentifier
) : IRequestErrorData;
