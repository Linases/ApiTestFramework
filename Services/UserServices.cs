using Modules;
using TestSettings;
using Wrappers;

namespace Services;

public class UserServices
{
    public static (HttpResponseMessage, UsersListVm) GetListUsers(int page) =>
        ReqresHttpClient.Get<UsersListVm>(new Uri($"{TestSettingsUrl.BaseUrlUsers}?page={page}"));

    public static (HttpResponseMessage, UsersListVm) GetDelayedUsers() =>
        ReqresHttpClient.Get<UsersListVm>(new Uri($"{TestSettingsUrl.BaseUrlUsers}?delay=3"));

    public static (HttpResponseMessage, CreateUserVm) PostCreate(JobNamePair model) =>
        ReqresHttpClient.PostOrPutOrPatch<CreateUserVm>(new Uri($"{TestSettingsUrl.BaseUrlUsers}"), model, HttpMethod.Post);

    public static (HttpResponseMessage, RegisterVm) Register(EmailPasswordPair model) =>
        ReqresHttpClient.PostOrPutOrPatch<RegisterVm>(new Uri($"{TestSettingsUrl.BaseUrlRegister}"), model, HttpMethod.Post);

    public static (HttpResponseMessage, SingleUserVm) GetSingleUser(int userId) =>
        ReqresHttpClient.Get<SingleUserVm>(new Uri($"{TestSettingsUrl.BaseUrlUsers}/{userId}"));

    public static HttpResponseMessage DeleteUser(int userId) =>
        ReqresHttpClient.Delete(new Uri($"{TestSettingsUrl.BaseUrlUsers}/{userId}"));

    public static (HttpResponseMessage, CreateUserVm) UpdateUser(int userId, JobNamePair model, HttpMethod method) =>
        ReqresHttpClient.PostOrPutOrPatch<CreateUserVm>(new Uri($"{TestSettingsUrl.BaseUrlUsers}/{userId}"), model, method);
}


