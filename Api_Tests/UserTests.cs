using NUnit.Framework;
using Services;

namespace Api_Tests;

public class UserTests
{
    private UserServices _userService;

    [SetUp]
    public void Setup()
    {
        _userService = new UserServices();
    }

    [Test]
    public async Task GetUsers_Page1_ShouldReturnCorrectUserIds()
    {
        var response = await _userService.GetUsersAsync(1);

        Assert.That(1, Is.EqualTo(response.Page));
        Assert.That(6, Is.EqualTo(response.Data.Count));
        Assert.That(Enumerable.Range(1, 6), Is.EqualTo(response.Data.Select(u => u.Id)));
    }
}