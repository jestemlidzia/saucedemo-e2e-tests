using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

using SauceDemo.Tests.Config;
using SauceDemo.Tests.Pages;

namespace SauceDemo.Tests.Fixtures;

public class BaseTest : PageTest {

    protected LoginPage _loginPage = null!;
    protected InventoryPage _inventoryPage = null!;

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

}
