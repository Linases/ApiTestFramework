using System.Net.Mime;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using Modules;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Wrappers;

public class ReqresHttpClient
{
    private static readonly HttpClient HttpClient;

     static ReqresHttpClient()
    {
        HttpClient = new HttpClient();
    }

    public ApiResult<TResult> GetApi<TResult>(Uri uri)
    {
      var response = GetHttpResponseMessage(uri);
      var responseContent = response.Content.ReadAsStringAsync().Result;

     try
      {
          return JsonConvert.DeserializeObject<ApiResult<TResult>>(responseContent)!;
      }
      catch
      {
          throw new HttpRequestException($"Api request failed: {responseContent}");
      }
    }


    public TResult Get<TResult>(Uri uri)
    {
        var builder = new UriBuilder(uri);
        var request = new HttpRequestMessage(HttpMethod.Get, builder.Uri);

        var response = HttpClient.SendAsync(request).Result;

        if (typeof(TResult) == typeof(HttpResponseMessage))
        {
            return (TResult)Convert.ChangeType(response, typeof(TResult));
        }

        return JsonConvert.DeserializeObject<TResult>(response.Content.ReadAsStringAsync().Result);
    }

    public TResult Post<TResult>(
        Uri uri,
        object content
    )
    {
        // var customSettings = new JsonSerializerSettings
        // {
        //     TypeNameHandling = TypeNameHandling.Auto,
        //     TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
        //     SerializationBinder = new DefaultSerializationBinder(),
        // };
        var contentJson = JsonConvert.SerializeObject(content);
        var responseMessage = SendRequest();

        try
        {
            return JsonConvert.DeserializeObject<TResult>(responseMessage.Content.ReadAsStringAsync().Result)!;
        }
        catch (Exception exception)
        {
            throw new HttpRequestException(
                $"Api request failed: {responseMessage.Content.ReadAsStringAsync().Result} Inner exception: {exception.Message}");
        }

        HttpResponseMessage SendRequest()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(contentJson, Encoding.UTF8, "application/json");

            var response = HttpClient.SendAsync(request).Result;

            return response;
        }
    }

    public TResult Delete<TResult>(Uri uri)
    {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            var response = HttpClient.SendAsync(request).Result;

        try
        {
            return JsonConvert.DeserializeObject<TResult>(response.Content.ReadAsStringAsync().Result);
        }
        catch (Exception e)
        {
            throw new HttpRequestException($"Api request failed: {response.Content.ReadAsStringAsync().Result} \n {e.Message}");
        }
    }


    public HttpResponseMessage GetHttpResponseMessage(Uri uri)
    {
        var builder = new UriBuilder(uri);

        var request = new HttpRequestMessage(HttpMethod.Get, builder.Uri);
        var response = HttpClient.SendAsync(request).Result;

        return response;
    }

    public HttpResponseMessage Delete(Uri uri)
    {
        var builder = new UriBuilder(uri);

        var request = new HttpRequestMessage(HttpMethod.Delete, builder.Uri);
        var response = HttpClient.SendAsync(request).Result;

        return response;
    }
}
