using NUnit.Framework;
using Services;

namespace Api_Tests;

public class UserTests
{
    private readonly UserServices _userService = new ();

    [Test]
    public void GetUsers_Page1_ShouldReturnCorrectUserIds()
    {
        var response = _userService.GetListUsers(1);
        var statusCode = _userService.GetStatusCode(2);
       Assert.That(statusCode.StatusCode, Is.EqualTo(""));


        Assert.That(1, Is.EqualTo(response.Page));
        Assert.That(6, Is.EqualTo(response.Data.Count));
        Assert.That(Enumerable.Range(1, 6), Is.EqualTo(response.Data.Select(u => u.Id)));
    }
}