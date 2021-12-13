namespace ReactRio.Utils.Api;

public class ResourceConflictException : RequestException
{
    public readonly string? ResourceIdentifier;
    public readonly string? ResourceIdentifierName;
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
        string resourceName, string resourceIdentifier, string resourceIdentifierName,
        string? message = null
    ) : base(message ?? GetMessage(resourceName, resourceIdentifier, resourceIdentifierName))
    {
        ResourceName = resourceName;
        ResourceIdentifier = resourceIdentifier;
        ResourceIdentifierName = resourceIdentifierName;
    }

    private static string GetMessage
        (string resoureName, string? resourceIdentifier = null, string? resourceIdentifierName = null)
    {
        if (resourceIdentifier is null || resourceIdentifierName is null)
            return $"Já existe um(a) \"{resoureName}\"";

        return $"Já existe um (a) \"{resoureName}\" com \"{resourceIdentifierName}\" igual a \"{resourceIdentifier}\"";
    }
}
