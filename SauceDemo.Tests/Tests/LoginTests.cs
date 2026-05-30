using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

using SauceDemo.Tests.Config;
using SauceDemo.Tests.Pages;
using SauceDemo.Tests.TestData;
using SauceDemo.Tests.Fixtures;

namespace SauceDemo.Tests;

public class LoginTest : BaseTest {

    [Test]
    public async Task TC01_SuccessfulLogin() {
        await _loginPage.LoginAsync(LoginTestData.StandardUser, LoginTestData.Password);
        await _inventoryPage.AssertInventoryPageVisibleAsync();
    }

    [Test]
    public async Task TC02_FailedLogin() {
        await _loginPage.LoginAsync(LoginTestData.NotExistingUser, LoginTestData.Password);
        await _loginPage.AssertErrorMessageAsync(LoginTestData.InvalidCredentialsError);
    }

    [Test]
    public async Task TC03_LoginAttemptWithLockedOutUser() {
        // test Failed
        // await _loginPage.LoginAsync(LoginTestData.StandardUser, LoginTestData.Password);
        await _loginPage.LoginAsync(LoginTestData.LockedOutUser, LoginTestData.Password);
        await _loginPage.AssertErrorMessageAsync(LoginTestData.LockedOutUserError);
    }

    [Test]
    public async Task TC04_LogoutAfterSuccessfulLogin() {
        await _loginPage.LoginAsync(LoginTestData.StandardUser, LoginTestData.Password);
        await _inventoryPage.AssertInventoryPageVisibleAsync();
        await _inventoryPage.LogoutAsync();
        await _loginPage.AssertLoginPageVisibleAsync();
    }

}
