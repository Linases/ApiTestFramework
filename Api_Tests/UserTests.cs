﻿using System.Net;
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
    private const int PageOneNumber = 1;
    private const int PageTwoNumber = 2;
    private const int TotalPages = 2;

    [Test]
    public void GetListUsersPageInfo()
    {
        // Verify page number, users per page, total users
        // Verify users count in "data" object
        // Verify status code
        var (firstPageUsersMessage, firstPageUsersInfo) = UserServices.GetListUsers(PageOneNumber);
        Assert.Multiple(() =>
        {
            Assert.That(firstPageUsersMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(firstPageUsersInfo.Page, Is.EqualTo(PageOneNumber));
            Assert.That(firstPageUsersInfo.PerPage, Is.EqualTo(6));
            Assert.That(firstPageUsersInfo.Total, Is.EqualTo(12));
            Assert.That(firstPageUsersInfo.Data.Count, Is.EqualTo(6));
            Assert.That(firstPageUsersInfo.Data.Select(u => u.Id), Is.EqualTo(Enumerable.Range(1, 6)));
        });

        // Verify Ids of users in "data" (if page = 1, user's ids should be - 1-6, if page =2, user's ids - 7-12)
        var (secondPageUsersMessage, secondPageUsersInfo) = UserServices.GetListUsers(PageTwoNumber);
        Assert.Multiple(() =>
        {
            Assert.That(secondPageUsersMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(secondPageUsersInfo.Data.Select(u => u.Id), Is.EqualTo(Enumerable.Range(7, 6)));
        });
    }

    [Test]
    public void GetSingleUserInfo()
    {
        // Verify all fields from model "data" and "support"
        // Verify status code
        var firstPageUsersData = UserServices.GetListUsers(1).Item2.Data;
        var secondUserFirstName = firstPageUsersData[1].FirstName;
        var secondUserLastName = firstPageUsersData[1].LastName;
        var secondUserEmail = firstPageUsersData[1].Email;
        var secondUserAvatar = firstPageUsersData[1].Avatar;

        var (singleUserMessage, singleUserInfo) = UserServices.GetSingleUser(UserId);
        var singleUserData = singleUserInfo.Data;
        var singleUserSupport = singleUserInfo.Support;
        Assert.Multiple(() =>
        {
            Assert.That(singleUserMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
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
        // Verify status code
        var statusCodeForSingleUser = UserServices.GetSingleUser(23);
        Assert.That(statusCodeForSingleUser.Item1.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public void CreateUser()
    {
        // Verify whole response model
        // Verify status code
        var model = new JobNamePair(Name, Job);
        var (responseMessage, createUserResponseInfo) = UserServices.PostCreate(model);
        Assert.Multiple(() =>
        {
            Assert.That(createUserResponseInfo.CreatedAt.Date.ToShortDateString(),
                Is.EqualTo(DateTime.Now.ToShortDateString()));
            Assert.That(createUserResponseInfo.Name, Is.EqualTo(Name));
            Assert.That(createUserResponseInfo.Id, Is.InstanceOf(typeof(int)));
            Assert.That(createUserResponseInfo.Job, Is.EqualTo(Job));
            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        });
    }

    [Test]
    public void PutUpdateUser()
    {
        // Verify whole response model
        var model = new JobNamePair(Name, UpdatedJob);
        var (updateMessage, responseUpdate) = UserServices.UpdateUser(2, model, HttpMethod.Put);
        VerifyUpdateResponse(updateMessage, responseUpdate);
    }

    [Test]
    public void PatchUpdateUser()
    {
        // Verify whole response model
        var model = new JobNamePair(Name, UpdatedJob);
        var (updateMessage, responseUpdate) = UserServices.UpdateUser(2, model, HttpMethod.Patch);
        VerifyUpdateResponse(updateMessage, responseUpdate);
    }

    [Test]
    public void DeleteUser()
    {
        // Verify whole response status code
        var responseDelete = UserServices.DeleteUser(UserId);
        Assert.That(responseDelete.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }

    [Test]
    public void RegisterUnsuccessful()
    {
        // Verify whole response model
        // Verify response status code
        var model = new EmailPasswordPair(Email, null);
        var (registerMessage, registerUserResponseInfo) = UserServices.Register(model);
        Assert.Multiple(() =>
        {
            Assert.That(registerMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(registerUserResponseInfo.Error, Is.EqualTo(Errors.MissingPassword));
        });
    }

    [Test]
    public void RegisterSuccessful()
    {
        // Verify whole response model
        // Verify response status code
        const int userId = 4;
        const string token = "QpwL5tke4Pnpja7X4";
        var model = new EmailPasswordPair(Email, Password);
        var (registerMessage, registerUserResponseInfo) = UserServices.Register(model);
        Assert.Multiple(() =>
        {
            Assert.That(registerUserResponseInfo.Id, Is.EqualTo(userId));
            Assert.That(registerUserResponseInfo.Token, Is.EqualTo(token));
            Assert.That(registerMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        });
    }

    [Test]
    public void GetDelayedResponse()
    {
        // Verify "total_pages" value
        var delayedUsersList = UserServices.GetDelayedUsers();
        Assert.That(delayedUsersList.Item2.TotalPages, Is.EqualTo(TotalPages));
    }

    private void VerifyUpdateResponse(HttpResponseMessage message, CreateUserVm response)
    {
        Assert.Multiple(() =>
        {
            Assert.That(response.Job, Is.EqualTo(UpdatedJob));
            Assert.That(response.Name, Is.EqualTo(Name));
            Assert.That(response.UpdatedAt.Date.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
            Assert.That(message.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        });
    }
}