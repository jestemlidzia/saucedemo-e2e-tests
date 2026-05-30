using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

using SauceDemo.Tests.Config;
using SauceDemo.Tests.Pages;

namespace SauceDemo.Tests.Fixtures;

public class BaseTest : PageTest {

    protected LoginPage _loginPage = null!;
    protected InventoryPage _inventoryPage = null!;

    [SetUp]
    public async Task SetUpAsync() {

        _loginPage = new LoginPage(Page);
        _inventoryPage = new InventoryPage(Page);

        await Context.Tracing.StartAsync(new() {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        await Page.GotoAsync(TestSettings.BaseUrl);
    }

    [TearDown]
    public async Task STearDownAsync() {
        var testStatus = TestContext.CurrentContext.Result.Outcome.Status;

        if (TestStatus.Failed == testStatus) {
            Directory.CreateDirectory(TestSettings.TracesPath);
            Directory.CreateDirectory(TestSettings.ScreenshotsPath);

            var testName = TestContext.CurrentContext.Test.Name;

            var screenshotPath = Path.Combine(
                TestSettings.ScreenshotsPath, $"{testName}.png"
            );

            await Page.ScreenshotAsync(new() {
                Path = screenshotPath,
                FullPage = true
            });

            TestContext.WriteLine($"Screenshot saved to: {screenshotPath}");

            var tracePath = Path.Combine(
                TestSettings.TracesPath, $"{testName}.zip"
            );

            await Context.Tracing.StopAsync(new() {
                Path = tracePath
            });

            TestContext.WriteLine($"Trace saved to: {tracePath}");
        }
        else {
            await Context.Tracing.StopAsync();
        }
    }

}
