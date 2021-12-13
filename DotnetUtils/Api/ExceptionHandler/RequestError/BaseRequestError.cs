using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ReactRio.Utils.Api;

public abstract class BaseRequestError<TErrorData> : IRequestError where TErrorData : IRequestErrorData
{
    protected BaseRequestError(Exception ex, bool isDevelopment)
    {
        if (!isDevelopment) return;

        var exceptionJsonString = JsonConvert.SerializeObject(ex);
        DevErrorData = JsonSerializer.Deserialize<dynamic>(exceptionJsonString);
    }

    public TErrorData Error { get; set; }

    [JsonPropertyName("__devError")]
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object DevErrorData { get; set; }

    public int Code { get; set; }
    public string Message { get; set; }
}
