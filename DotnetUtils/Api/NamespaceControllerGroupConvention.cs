using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ReactRio.Utils.Api;

public class NamespaceControllerGroupConvention : IControllerModelConvention
{
    private readonly Dictionary<string, string> _namespaceGroupMap;
    private readonly Dictionary<string, string> _namespacePrefixMap;

    public NamespaceControllerGroupConvention
    (
        Dictionary<string, string> namespaceGroupMap,
        Dictionary<string, string> namespacePrefixMap
    )
    {
        _namespaceGroupMap = namespaceGroupMap;
        _namespacePrefixMap = namespacePrefixMap;
    }

    public void Apply(ControllerModel controller)
    {
        ApplyGroupName(controller);
        ApplyPrefix(controller);
    }

    private void ApplyPrefix(ControllerModel controller)
    {
        var controllerType = controller.ControllerType;
        var name = controllerType.Namespace!.Split('.').Last();
        var firstSelector = controller.Selectors.First();
        var attribute = firstSelector.AttributeRouteModel ??= new AttributeRouteModel();

        if (!_namespacePrefixMap.ContainsKey(name))
            throw new Exception($"Controller em namespace fora do padrao: {controllerType.FullName}");

        var prefix = _namespacePrefixMap[name];
        attribute.Template = $"{prefix}{attribute.Template}";
    }

    private void ApplyGroupName(ControllerModel controller)
    {
        var controllerType = controller.ControllerType;
        var name = controllerType.Namespace!.Split('.').Last();

        if (!_namespaceGroupMap.ContainsKey(name))
            throw new Exception($"Controller em namespace fora do padrao: {controllerType.FullName}");

        var group = _namespaceGroupMap[name];
        controller.ApiExplorer.GroupName = group;
    }
}
