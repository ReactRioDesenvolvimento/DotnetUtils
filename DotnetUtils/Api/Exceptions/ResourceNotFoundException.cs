namespace ReactRio.Utils.Api;

public class ResourceNotFoundException : RequestException
{
    public readonly string? ResourceIdentifier;
    public readonly string? ResourceIdentifierName;
    public readonly string ResourceName;

    public ResourceNotFoundException(string resourceName) : base(GetMessage(resourceName))
    {
        ResourceName = resourceName;
    }

    public ResourceNotFoundException(string resourceName, string message) : base(message)
    {
        ResourceName = resourceName;
    }

    public ResourceNotFoundException
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
            return $"O(a) \"{resoureName}\" não existe";

        return $"Não existe o(a) \"{resoureName}\" com \"{resourceIdentifierName}\" igual a \"{resourceIdentifier}\"";
    }
}
