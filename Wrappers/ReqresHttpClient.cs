using System.Text;
using Newtonsoft.Json;

namespace Wrappers;

public static class ReqresHttpClient
{
    private static readonly HttpClient HttpClient;

     static ReqresHttpClient()
    {
        HttpClient = new HttpClient();
    }

    public static TResult Get<TResult>(Uri uri)
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

    public static (HttpResponseMessage responce,TResult result) PostOrPutOrPatch<TResult>(
        Uri uri,
        object content,
        HttpMethod httpMethod
    )
    {
        var contentJson = JsonConvert.SerializeObject(content);
        var responseMessage = SendRequest();

        try
        {
           TResult result = JsonConvert.DeserializeObject<TResult>(responseMessage.Content.ReadAsStringAsync().Result)!;
           return (responseMessage,result);
        }
        catch (Exception exception)
        {
            throw new HttpRequestException(
                $"Api request failed: {responseMessage.Content.ReadAsStringAsync().Result} Inner exception: {exception.Message}");
        }

        HttpResponseMessage SendRequest()
        {
            var request = new HttpRequestMessage(httpMethod, uri);
            request.Content = new StringContent(contentJson, Encoding.UTF8, "application/json");

            var response = HttpClient.SendAsync(request).Result;

            return response;
        }
    }

    public static  HttpResponseMessage GetHttpResponseMessage(Uri uri)
    {
        var builder = new UriBuilder(uri);

        var request = new HttpRequestMessage(HttpMethod.Get, builder.Uri);
        var response = HttpClient.SendAsync(request).Result;

        return response;
    }

    public static HttpResponseMessage Delete(Uri uri)
    {
        var builder = new UriBuilder(uri);

        var request = new HttpRequestMessage(HttpMethod.Delete, builder.Uri);
        var response = HttpClient.SendAsync(request).Result;

        return response;
    }
}
