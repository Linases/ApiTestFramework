using System.Text.Json;

namespace Wrappers;

public class ReqresHttpClient
{
    private readonly HttpClient _httpClient;

    public ReqresHttpClient()
    {
        _httpClient = new HttpClient();
    }

    public async Task<T> GetAsync<T>(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content);
    }
}
