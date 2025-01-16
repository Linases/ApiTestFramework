using Modules;
using Wrappers;

namespace Services;

public class UserServices
{
    private const string BaseUrl = "https://reqres.in/api";
    private static readonly ReqresHttpClient HttpClient = new();

    public static UsersList GetListUsers(int page) => HttpClient.Get<UsersList>(new Uri($"{BaseUrl}/users?page={page}"));

    public static HttpResponseMessage GetStatusCode(int page) =>
        HttpClient.GetHttpResponseMessage(new Uri($"{BaseUrl}/users?page={page}"));
}



