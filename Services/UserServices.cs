using Wrappers;

namespace Services;

public class UserServices
{
    private const string BaseUrl = "https://reqres.in/api/";
    private readonly ReqresHttpClient _httpClient = new();

    public UsersList GetListUsers(int page) => _httpClient.Get<UsersList>(new Uri($"{BaseUrl}/users?page={page}")).Model;

    public HttpResponseMessage GetStatusCode(int page) =>
        _httpClient.GetHttpResponseMessage(new Uri($"{BaseUrl}/users?page={page}"));
}

public class UsersList
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
    public List<User> Data { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}