using Microsoft.Extensions.Configuration;

namespace ReactRio.Utils.Api;

public static class DotEnvExtensions
{
    public static IConfigurationBuilder AddDotEnv
    (
        this IConfigurationBuilder configurationBuilder,
        string filePath, string prefix
    )
    {
        configurationBuilder.Add(new DotEnvConfigurationSource(filePath, prefix));

        return configurationBuilder;
    }
}

public class DotEnvConfigurationSource : IConfigurationSource
{
    private readonly string _filePath;
    private readonly string _prefix;

    public DotEnvConfigurationSource(string filePath, string prefix)
    {
        _filePath = filePath;
        _prefix = prefix;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new DotEnvConfigurationProvider(_filePath, _prefix);
    }
}

public class DotEnvConfigurationProvider : ConfigurationProvider
{
    public DotEnvConfigurationProvider(string filePath, string prefix)
    {
        if (!File.Exists(filePath))
            return;

        Data = new Dictionary<string, string>();

        foreach (var line in File.ReadAllLines(filePath))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var keySeparatorPosition = line.IndexOf('=');
            var key = line[..keySeparatorPosition].Replace(prefix, "");
            var value = line[( keySeparatorPosition + 1 )..];

            Data[key] = value;
        }
    }
}
