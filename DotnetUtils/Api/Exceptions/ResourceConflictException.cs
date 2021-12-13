namespace ReactRio.Utils.Api;

public class ResourceConflictException : RequestException
{
    public readonly string ConflictingPropertyName;
    public readonly string ConflictingPropertyValue;
    public readonly string ResourceName;

    public ResourceConflictException(string message) : base(message)
    { }

    public ResourceConflictException
    (
        string resourceName,
        string conflictingPropertyName,
        string conflictingPropertyValue
    ) : base(GetMessage(resourceName, conflictingPropertyName, conflictingPropertyValue))
    {
        ResourceName = resourceName;
        ConflictingPropertyName = conflictingPropertyName;
        ConflictingPropertyValue = conflictingPropertyValue;
    }

    private static string GetMessage
    (
        string resourceName, string conflictingPropertyName,
        string conflictingPropertyValue
    )
    {
        return
            $"Já existe um recurso do tipo '{resourceName}' com a propriedade {conflictingPropertyName} com valor {conflictingPropertyValue}";
    }
}
