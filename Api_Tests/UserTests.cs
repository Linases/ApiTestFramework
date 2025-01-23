using System.Net;
using Constants;
using Modules;
using NUnit.Framework;
using Services;

namespace Api_Tests;

public class UserTests
{
    private const string Name = "morpheus";
    private const string Job = "leader";
    private const string UpdatedJob = "zion resident";
    private const string Email = "eve.holt@reqres.in";
    private const string Password = "pistol";
    private const int UserId = 2;
    private const int FirstPageNr = 1;
    private const int SecondPageNr = 2;

    [Test]
    public void GetListUsersPageInfo()
    {
        var firstPageUsers = UserServices.GetListUsers(FirstPageNr);
        var statusCodeForFirstPage = UserServices.GetStatusCodeForListUsers(FirstPageNr);
        Assert.Multiple(() =>
        {
            Assert.That(statusCodeForFirstPage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(firstPageUsers.Page, Is.EqualTo(FirstPageNr));
            Assert.That(firstPageUsers.PerPage, Is.EqualTo(6));
            Assert.That(firstPageUsers.Total, Is.EqualTo(12));
            Assert.That(firstPageUsers.Data.Count, Is.EqualTo(6));
            Assert.That(firstPageUsers.Data.Select(u => u.Id), Is.EqualTo(Enumerable.Range(1, 6)));
        });

        // Get second page
        var secondList = UserServices.GetListUsers(SecondPageNr);
        var statusCodeForSecondPage = UserServices.GetStatusCodeForListUsers(SecondPageNr);
        var selectedIds = secondList.Data.Select(u => u.Id);
        Assert.That(statusCodeForSecondPage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(selectedIds, Is.EqualTo(Enumerable.Range(7, 6)));

    }

    [Test]
    public void GetSingleUserInfo()
    {
        var firstPageUsersData = UserServices.GetListUsers(1).Data;
        var secondUserFirstName = firstPageUsersData[1].FirstName;
        var secondUserLastName = firstPageUsersData[1].LastName;
        var secondUserEmail = firstPageUsersData[1].Email;
        var secondUserAvatar = firstPageUsersData[1].Avatar;
        var statusCodeForSingleUser = UserServices.GetStatusCodeForSingleUser(UserId);
        Assert.That(statusCodeForSingleUser.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var singleUserData = UserServices.GetSingleUser(UserId).Data;
        var singleUserSupport = UserServices.GetSingleUser(UserId).Support;
        Assert.Multiple(() =>
        {
            Assert.That(singleUserData.Id, Is.EqualTo(UserId));
            Assert.That(singleUserData.Email, Is.EqualTo(secondUserEmail));
            Assert.That(singleUserData.FirstName, Is.EqualTo(secondUserFirstName));
            Assert.That(singleUserData.LastName, Is.EqualTo(secondUserLastName));
            Assert.That(singleUserData.Avatar, Is.EqualTo(secondUserAvatar));
            Assert.That(singleUserData.Id, Is.EqualTo(UserId));
            Assert.That(singleUserSupport.Text, Is.EqualTo(SupportFields.Text));
            Assert.That(singleUserSupport.Url, Is.EqualTo(SupportFields.Url));
        });
    }

    [Test]
    public void GetSingleNotFound()
    {
        var statusCodeForSingleUser = UserServices.GetStatusCodeForSingleUser(23);
        Assert.That(statusCodeForSingleUser.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public void CreateUser()
    {
        var model = new JobNamePair(Name, Job);
        var createdUserResponse = UserServices.PostCreate(model);
        var responseMessage = createdUserResponse.Item2;
        var responseMessageStatus = createdUserResponse.Item1;
        Assert.That(responseMessage.CreatedAt.Date.ToShortDateString(),
            Is.EqualTo(DateTime.Now.ToShortDateString()));
        Assert.That(responseMessage.Name, Is.EqualTo(Name));
        Assert.That(responseMessage.Id, Is.InstanceOf(typeof(int)));
        Assert.That(responseMessage.Job, Is.EqualTo(Job));
        Assert.That(responseMessageStatus.StatusCode, Is.EqualTo(HttpStatusCode.Created));
    }

    [Test]
    public void GetDelayedResponse()
    {
        var delayedUsersList = UserServices.GetDelayedUsers();
        Assert.That(delayedUsersList.TotalPages, Is.EqualTo(2));
    }

    [Test]
    public void PutUpdateUser()
    {
        var model = new JobNamePair(Name, UpdatedJob);
        var responseUpdate= UserServices.UpdateUser(2,model, HttpMethod.Put);
        VerifyUpdateResponse(responseUpdate);
    }

    [Test]
    public void PatchUpdateUser()
    {
        var model = new JobNamePair(Name, UpdatedJob);
        var responseUpdate= UserServices.UpdateUser(2,model, HttpMethod.Patch);
        VerifyUpdateResponse(responseUpdate);
    }

    [Test]
    public void RegisterUnsuccessful()
    {
        var model = new RegistrationPair(Email, null);
        var response = UserServices.Register(model);
        Assert.That(response.Item1.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(response.Item2.Error, Is.EqualTo(Errors.MissingPassword));
    }

    [Test]
    public void RegisterSuccessful()
    {
        var model = new RegistrationPair(Email, Password);
        var response = UserServices.Register(model);
        Assert.That(response.Item2.Id, Is.EqualTo(4));
        Assert.That(response.Item2.Token, Is.EqualTo("QpwL5tke4Pnpja7X4"));
        Assert.That( response.Item1.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    private void VerifyUpdateResponse((HttpResponseMessage,CreateUserVm) response)
    {
        Assert.That(response.Item2.Job, Is.EqualTo(UpdatedJob));
        Assert.That(response.Item2.Name, Is.EqualTo(Name));
        Assert.That(response.Item2.UpdatedAt.Date.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
        Assert.That( response.Item1.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
