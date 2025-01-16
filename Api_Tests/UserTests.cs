using System.Net;
using NUnit.Framework;
using Services;

namespace Api_Tests;

public class UserTests
{

    [Test]
    public void GetUsersPageInfo()
    {
        var firstPageUsers = UserServices.GetListUsers(1);
        var statusCodeForFirstPage = UserServices.GetStatusCode(2);
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
        var statusCodeForSecondPage = UserServices.GetStatusCode(2);
        var selectedIds = secondList.Data.Select(u => u.Id);
        Assert.That(statusCodeForSecondPage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(selectedIds, Is.EqualTo(Enumerable.Range(7, 6)));
    }
}