using Modules;
using Wrappers;

namespace Services;

public class UserServices
{
    private const string BaseUrl = "https://reqres.in/api";

    public static UsersListVm GetListUsers(int page) => ReqresHttpClient.Get<UsersListVm>(new Uri($"{BaseUrl}/users?page={page}"));

    public static UsersListVm GetDelayedUsers() => ReqresHttpClient.Get<UsersListVm>(new Uri($"{BaseUrl}/users?delay=3"));

    public static (HttpResponseMessage,CreateUserVm) PostCreate(JobNamePair model) =>
        ReqresHttpClient.PostOrPutOrPatch<CreateUserVm>(new Uri($"{BaseUrl}/users"), model, HttpMethod.Post);

    public static (HttpResponseMessage, RegisterVm) Register(RegistrationPair model) =>
        ReqresHttpClient.PostOrPutOrPatch<RegisterVm>(new Uri($"{BaseUrl}/register"), model, HttpMethod.Post );

    public static HttpResponseMessage GetStatusCodeForListUsers(int page) =>
        ReqresHttpClient.GetHttpResponseMessage(new Uri($"{BaseUrl}/users?page={page}"));

    public static HttpResponseMessage GetStatusCodeForSingleUser(int userId) =>
        ReqresHttpClient.GetHttpResponseMessage(new Uri($"{BaseUrl}/users/{userId}"));

    public static SingleUserVm GetSingleUser(int userId) => ReqresHttpClient.Get<SingleUserVm>(new Uri($"{BaseUrl}/users/{userId}"));

    public static HttpResponseMessage DeleteUser (int userId) => ReqresHttpClient.Delete(new Uri($"{BaseUrl}/users/{userId}"));

    public static (HttpResponseMessage,CreateUserVm) UpdateUser(int userId, JobNamePair model, HttpMethod method) => ReqresHttpClient.PostOrPutOrPatch<CreateUserVm>(new Uri($"{BaseUrl}/users/{userId}"), model, method);
  }




