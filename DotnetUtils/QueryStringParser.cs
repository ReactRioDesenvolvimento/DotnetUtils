using System.Text.Json;
using System.Web;

namespace ReactRio.Utils;

public static class QueryString
{
    public static string FromObject(object obj)
    {
        var serialized = JsonSerializer.Serialize(obj);
        var keyValues = JsonSerializer.Deserialize<IDictionary<string, object>>(serialized)!;
        var encodedKeyValues = keyValues.Select(entry =>
            $"{HttpUtility.UrlEncode(entry.Key)}={HttpUtility.UrlEncode(entry.Value.ToString())}");

        return string.Join("&", encodedKeyValues);
    }
}
