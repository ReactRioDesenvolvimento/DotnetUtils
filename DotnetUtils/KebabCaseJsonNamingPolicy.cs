using System.Text.Json;

namespace ReactRio.DotnetUtils;

public sealed class KebabCaseJsonNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        if (string.IsNullOrEmpty(name)) return name;

        var builder = new StringBuilder();
        builder.Append(char.ToLower(name[0]));

        for (var i = 1; i < name.Length; i++)
        {
            var c = name[i];

            if (char.IsLower(c))
            {
                builder.Append(char.ToLower(c));

                continue;
            }

            var previousChar = name[i - 1];
            if (char.IsLower(previousChar))
                builder.Append('-');
            else if (i + 1 < name.Length && char.IsLower(name[i + 1])) builder.Append('-');

            builder.Append(char.ToLower(c));
        }

        return builder.ToString();
    }
}
