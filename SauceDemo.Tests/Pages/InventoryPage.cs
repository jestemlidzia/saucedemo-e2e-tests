using Microsoft.Playwright;

namespace SauceDemo.Tests.Pages;

public class InventoryPage {

    private readonly IPage _page;

    public InventoryPage(IPage page) {
        _page = page;
    }

    private ILocator PageTitle => _page.GetByText("Products");
    private ILocator InventoryContainer => _page.Locator("[data-test='inventory-container']");
    private ILocator OpenMenuButton => _page.GetByRole(AriaRole.Button, new() { Name = "Open Menu" });
    private ILocator LogoutButton => _page.GetByText("Logout");

    public async Task AssertInventoryPageVisibleAsync() {
        await Assertions.Expect(PageTitle).ToBeVisibleAsync();
        await Assertions.Expect(InventoryContainer).ToBeVisibleAsync();
    }

    public async Task LogoutAsync() {
        await OpenMenuButton.ClickAsync();
        await LogoutButton.ClickAsync();
    }

}