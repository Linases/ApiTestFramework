using System.Net;
using Constants;
using Modules;
using Newtonsoft.Json;
using NUnit.Framework;
using Services;

namespace Api_Tests;

public class UserTests
{

    [Test]
    public void GetListUsersPageInfo()
    {
        var firstPageUsers = UserServices.GetListUsers(1);
        var statusCodeForFirstPage = UserServices.GetStatusCodeForListUsers(1);
        Assert.Multiple(() =>
        {
            Assert.That(statusCodeForFirstPage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(firstPageUsers.Page, Is.EqualTo(1));
            Assert.That(firstPageUsers.PerPage, Is.EqualTo(6));
            Assert.That(firstPageUsers.Total, Is.EqualTo(12));
            Assert.That(firstPageUsers.Data.Count, Is.EqualTo(6));
            Assert.That(firstPageUsers.Data.Select(u => u.Id), Is.EqualTo(Enumerable.Range(1, 6)));
        });

        // Get second page
        var secondList = UserServices.GetListUsers(2);
        var statusCodeForSecondPage = UserServices.GetStatusCodeForListUsers(2);
        var selectedIds = secondList.Data.Select(u => u.Id);
        Assert.That(statusCodeForSecondPage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(selectedIds, Is.EqualTo(Enumerable.Range(7, 6)));

    }

    [Test]
    public void GetSingleUserInfo()
    {
        const int secondUserId = 2;
        var firstPageUsersData = UserServices.GetListUsers(1).Data;
        var secondUserFirstName = firstPageUsersData[1].FirstName;
        var secondUserLastName = firstPageUsersData[1].LastName;
        var secondUserEmail = firstPageUsersData[1].Email;
        var secondUserAvatar = firstPageUsersData[1].Avatar;
        var statusCodeForSingleUser = UserServices.GetStatusCodeForSingleUser(2);
        Assert.That(statusCodeForSingleUser.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var singleUserData = UserServices.GetSingleUser(2).Data;
        var singleUserSupport = UserServices.GetSingleUser(2).Support;
        Assert.Multiple(() =>
        {
            Assert.That(singleUserData.Id, Is.EqualTo(secondUserId));
            Assert.That(singleUserData.Email, Is.EqualTo(secondUserEmail));
            Assert.That(singleUserData.FirstName, Is.EqualTo(secondUserFirstName));
            Assert.That(singleUserData.LastName, Is.EqualTo(secondUserLastName));
            Assert.That(singleUserData.Avatar, Is.EqualTo(secondUserAvatar));
            Assert.That(singleUserData.Id, Is.EqualTo(secondUserId));
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
        var model = new JobNamePairVm("morpheus", "leader");
        var createdUserResponse = UserServices.PostCreate(model);
        Assert.That(createdUserResponse.CreatedAt.Date.ToShortDateString(),
            Is.EqualTo(DateTime.Now.ToShortDateString()));
        Assert.That(createdUserResponse.Name, Is.EqualTo("morpheus"));
        Assert.That(createdUserResponse.Id, Is.InstanceOf(typeof(int)));
        Assert.That(createdUserResponse.Job, Is.EqualTo("leader"));

        // Assert.That(createdUserResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public void GetDelayedResponse()
    {
        var delayedUsersList = UserServices.GetDelayedUsers();
        Assert.That(delayedUsersList.TotalPages, Is.EqualTo(2));
    }

    [Test]
    public void DeleteUser()
    {
        var responce = UserServices.DeleteUser(2);
        Assert.That(responce.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }

    [Test]
    public void RegisterUnsuccessful()
    {
        var model = new RegistrationPair("sydney@fife", null);
        var responce = UserServices.Register(model);
      //  Assert.That(responce.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }

    [Test]
    public void RegisterSuccessful()
    {
        var model = new RegistrationPair("eve.holt@reqres.in@fife", "pistol");
        var responce = UserServices.Register(model);
        Assert.That(responce.Email, Is.EqualTo(HttpStatusCode.NoContent));
        Assert.That(responce.Email, Is.EqualTo(HttpStatusCode.NoContent));
    }
}
