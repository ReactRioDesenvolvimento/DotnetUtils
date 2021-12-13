namespace ReactRio.Utils.Api;

public class ResourceNotFoundException : RequestException
{
    public readonly string ResourceIdentifier;
    public readonly string ResourceIdentifierName;
    public readonly string ResourceName;

    public ResourceNotFoundException(string resourceName, string resourceIdentifierName, string resourceIdentifier)
        : base(GetMessage(resourceName, resourceIdentifierName, resourceIdentifier))
    {
        ResourceName = resourceName;
        ResourceIdentifierName = resourceIdentifierName;
        ResourceIdentifier = resourceIdentifier;
    }

    private static string GetMessage(string resourceName, string identifierName, string identifier)
    {
        return $"Resource '{resourceName}' with '{identifierName}' identifier of '{identifier}' not found.";
    }
}
