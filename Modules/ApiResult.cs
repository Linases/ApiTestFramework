using Newtonsoft.Json;

namespace Modules;

public class ApiResult
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }

}

public class ApiResult<T> : ApiResult
{
   [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public T Model { get; set; }

}