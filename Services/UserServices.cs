using Wrappers;

namespace Services;

public class UserServices
{
    private readonly ReqresHttpClient _httpClientWrapper;

    public UserServices()
    {
        _httpClientWrapper = new ReqresHttpClient();
    }

    public async Task<UserListResponse> GetUsersAsync(int page)
    {
        return await _httpClientWrapper.GetAsync<UserListResponse>($"https://reqres.in/api/users?page={page}");
    }
}

public class UserListResponse
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