using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

using SauceDemo.Tests.Config;

namespace SauceDemo.Tests;

public class LoginTest : PageTest {

    [SetUp]
    public async Task SetUpAsync() {
        TestContext.WriteLine("Setup...");
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
        TestContext.WriteLine("TEST 01");
    }

    [Test]
    public async Task TC02_FailedLogin() {
        TestContext.WriteLine("TEST 02");
    }

    [Test]
    public async Task TC03_LoginAttemptWithLockedOutUser() {
        TestContext.WriteLine("TEST 03");
    }

    [Test]
    public async Task TC04_LogoutAfterSuccessfulLogin() {
        TestContext.WriteLine("TEST 04");
    }

}
