namespace ReactRio.Utils.Api;

public class ResourceConflictException : RequestException
{
    public readonly string? ConflictingPropertyName;
    public readonly string? ConflictingPropertyValue;
    public readonly string ResourceName;

    public ResourceConflictException(string resourceName) : base(GetMessage(resourceName))
    {
        ResourceName = resourceName;
    }

    public ResourceConflictException(string resourceName, string message) : base(message)
    {
        ResourceName = resourceName;
    }

    public ResourceConflictException
    (
        string resourceName, string conflictingPropertyValue, string conflictingPropertyName,
        string? message = null
    ) : base(message ?? GetMessage(resourceName, conflictingPropertyValue, conflictingPropertyName))
    {
        ResourceName = resourceName;
        ConflictingPropertyValue = conflictingPropertyValue;
        ConflictingPropertyName = conflictingPropertyName;
    }

    private static string GetMessage
        (string resoureName, string? resourceIdentifier = null, string? resourceIdentifierName = null)
    {
        if (resourceIdentifier is null || resourceIdentifierName is null)
            return $"Já existe um(a) \"{resoureName}\"";

        return $"Já existe um (a) \"{resoureName}\" com \"{resourceIdentifierName}\" igual a \"{resourceIdentifier}\"";
    }
}
