using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

using SauceDemo.Tests.Config;
using SauceDemo.Tests.Pages;

namespace SauceDemo.Tests;

public class LoginTest : PageTest {

    private LoginPage _loginPage = null!;
    [SetUp]
    public async Task SetUpAsync() {
        TestContext.WriteLine("Setup...");
        _loginPage = new LoginPage(Page);
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
        await _loginPage.LoginAsync("standard_user", "secret_sauce");
        # TODO: check invetory page visibility
    }

    [Test]
    public async Task TC02_FailedLogin() {
        await _loginPage.LoginAsync("not_existing_user", "secret_sauce");
        await _loginPage.AssertErrorMessageAsync("Epic sadface: Username and password do not match any user in this service");
    }

    [Test]
    public async Task TC03_LoginAttemptWithLockedOutUser() {
        await _loginPage.LoginAsync("standard_user", "Epic sadface: Sorry, this user has been locked out.");
    }

    [Test]
    public async Task TC04_LogoutAfterSuccessfulLogin() {
        await _loginPage.LoginAsync("locked_out_user", "secret_sauce");
        # TODO: check invetory page visibility
        # logut 
        # await _loginPage.AssertErrorMessageAsync();
    }

}
