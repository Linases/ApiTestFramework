using System.Text.Json;
using Modules;

namespace Wrappers;

public class ReqresHttpClient
{
    private readonly HttpClient _httpClient;

    public ReqresHttpClient()
    {
        _httpClient = new HttpClient();
    }

    public ApiResult<TResult> Get<TResult>(Uri uri)
    {
      var response = GetHttpResponseMessage(uri);
        //response.EnsureSuccessStatusCode();

        var responseContent = JsonSerializer.Deserialize<ApiResult<TResult>>(response.Content.ReadAsStringAsync().Result)!;

        return responseContent;
    }

    public HttpResponseMessage GetHttpResponseMessage(Uri uri)
    {
        var builder = new UriBuilder(uri);

        var request = new HttpRequestMessage(HttpMethod.Get, builder.Uri);
        var response = _httpClient.SendAsync(request).Result;

        return response;
    }

}
