﻿using System.Text;
using Newtonsoft.Json;

namespace Wrappers;

public static class ReqresHttpClient
{
    private static readonly HttpClient HttpClient;

    static ReqresHttpClient()
    {
        HttpClient = new HttpClient();
    }

    public static (HttpResponseMessage response, TResult result) Get<TResult>(Uri uri)
    {
        var builder = new UriBuilder(uri);
        var request = new HttpRequestMessage(HttpMethod.Get, builder.Uri);
        var response = HttpClient.SendAsync(request).Result;

        try
        {
            var result = JsonConvert.DeserializeObject<TResult>(response.Content.ReadAsStringAsync().Result)!;

            return (response, result);
        }
        catch (Exception exception)
        {
            throw new HttpRequestException(
                $"Api request failed: {response.Content.ReadAsStringAsync().Result} Inner exception: {exception.Message}");
        }
    }

    public static (HttpResponseMessage response, TResult result) PostOrPutOrPatch<TResult>(Uri uri, object content,
        HttpMethod httpMethod)
    {
        var contentJson = JsonConvert.SerializeObject(content);
        var responseMessage = SendRequest();

        try
        {
            var result =
                JsonConvert.DeserializeObject<TResult>(responseMessage.Content.ReadAsStringAsync().Result)!;

            return (responseMessage, result);
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

    public static HttpResponseMessage Delete(Uri uri)
    {
        var builder = new UriBuilder(uri);

        var request = new HttpRequestMessage(HttpMethod.Delete, builder.Uri);
        var response = HttpClient.SendAsync(request).Result;

        return response;
    }
}