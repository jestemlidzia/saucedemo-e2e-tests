using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

using SauceDemo.Tests.Config;
using SauceDemo.Tests.Pages;
using SauceDemo.Tests.TestData;

namespace SauceDemo.Tests.Fixtures;

public class AuthenticatedBaseTest : PageTest {

    protected InventoryPage _inventoryPage = null!;
    protected CartPage _cartPage = null!;
    protected CheckoutPage _checkoutPage = null!;
    protected ProductDetailsPage _productDetailsPage = null!;

    [OneTimeSetUp]
    public async Task CreateStorageStateAsync() {
        if (!File.Exists(TestSettings.StorageStatePath)) {
            await SaveStorageStateAsync();
        }
    }

    public override BrowserNewContextOptions ContextOptions() {
        return new BrowserNewContextOptions {
            BaseURL = TestSettings.BaseUrl,
            StorageStatePath = TestSettings.StorageStatePath
        };
    }

    private static async Task SaveStorageStateAsync() {
        Directory.CreateDirectory(
            Path.GetDirectoryName(TestSettings.StorageStatePath)!);

        using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync(new() {
            Headless = TestSettings.Headless
        });

        var context = await browser.NewContextAsync(new() {
            BaseURL = TestSettings.BaseUrl
        });

        var page = await context.NewPageAsync();
        var loginPage = new LoginPage(page);

        await page.GotoAsync("/");
        await loginPage.LoginAsync(LoginTestData.StandardUser, LoginTestData.Password);
        await page.WaitForURLAsync("**/inventory.html");

        await context.StorageStateAsync(new() {
            Path = TestSettings.StorageStatePath
        });

        await context.CloseAsync();
    }

    [SetUp]
    public async Task SetUpAsync() {

        _inventoryPage = new InventoryPage(Page);
        _cartPage = new CartPage(Page);
        _checkoutPage = new CheckoutPage(Page);
        _productDetailsPage = new ProductDetailsPage(Page);

        await Context.Tracing.StartAsync(new() {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        await Page.GotoAsync("/inventory.html");
    }

    [TearDown]
    public async Task TearDownAsync() {
        var testStatus = TestContext.CurrentContext.Result.Outcome.Status;

        if (TestStatus.Failed == testStatus) {
            Directory.CreateDirectory(TestSettings.TracesPath);
            Directory.CreateDirectory(TestSettings.ScreenshotsPath);

            var testName = TestContext.CurrentContext.Test.Name;

            var tracePath = Path.Combine(
                TestSettings.TracesPath, $"{testName}.zip"
            );

            await Context.Tracing.StopAsync(new() {
                Path = tracePath
            });

            TestContext.WriteLine($"Trace saved to: {tracePath}");

            var screenshotPath = Path.Combine(
                TestSettings.ScreenshotsPath, $"{testName}.png"
            );

            await Page.ScreenshotAsync(new() {
                Path = screenshotPath,
                FullPage = true
            });

            TestContext.WriteLine($"Screenshot saved to: {screenshotPath}");
        }
        else {
            await Page.ScreenshotAsync();
        }
    }

}
