using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

using SauceDemo.Tests.Config;
using SauceDemo.Tests.Pages;
using SauceDemo.Tests.TestData;

namespace SauceDemo.Tests;

public class LoginTest : PageTest {

    private LoginPage _loginPage = null!;
    private InventoryPage _inventoryPage = null!;

    [SetUp]
    public async Task SetUpAsync() {
        TestContext.WriteLine("Setup...");
        _loginPage = new LoginPage(Page);
        _inventoryPage = new InventoryPage(Page);
        await Page.GotoAsync(TestSettings.BaseUrl);
    }

    [TearDown]
    public async Task STearDownAsync() {
        TestContext.WriteLine("Clean up...");
        await Page.ScreenshotAsync(new()
        {
            Path = "screenshot.png"
        });
    }

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
