using System.Text.Json;
using Modules;
using Newtonsoft.Json;

namespace Wrappers;

public class ReqresHttpClient
{
    private static readonly HttpClient HttpClient;

     static ReqresHttpClient()
    {
        HttpClient = new HttpClient();
    }

    // public ApiResult<TResult> Get<TResult>(Uri uri)
    // {
    //   var response = GetHttpResponseMessage(uri);
    //   var responseContent = response.Content.ReadAsStringAsync().Result;
    //
    //  try
    //   {
    //       return JsonConvert.DeserializeObject<ApiResult<TResult>>(responseContent)!;
    //   }
    //   catch
    //   {
    //       throw new HttpRequestException($"Api request failed: {responseContent}");
    //   }
    // }

    public TResult Get<TResult>(Uri uri)
    {
        var builder = new UriBuilder(uri);
        var request = new HttpRequestMessage(HttpMethod.Get, builder.Uri);

        var response = HttpClient.SendAsync(request).Result;

        if (typeof(TResult) == typeof(HttpResponseMessage))
        {
            return (TResult)Convert.ChangeType(response, typeof(TResult));
        }

        return JsonConvert.DeserializeObject<TResult>(response.Content.ReadAsStringAsync().Result)!;
    }

    public HttpResponseMessage GetHttpResponseMessage(Uri uri)
    {
        var builder = new UriBuilder(uri);

        var request = new HttpRequestMessage(HttpMethod.Get, builder.Uri);
        var response = HttpClient.SendAsync(request).Result;

        return response;
    }
}
