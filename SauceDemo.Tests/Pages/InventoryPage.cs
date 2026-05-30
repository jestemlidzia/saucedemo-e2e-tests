using Microsoft.Playwright;
using System.Globalization;

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
    private ILocator ProductSortMenuButton => _page.Locator("[data-test='product-sort-container']");
    private ILocator ActiveSortOption => _page.Locator("[data-test='active-option']");
    private ILocator ProductPrices => _page.Locator("[data-test='inventory-item-price']");
    private ILocator shoppingCartButton => _page.Locator("[data-test='shopping-cart-link']");
    
    public async Task AssertInventoryPageVisibleAsync() {
        await Assertions.Expect(PageTitle).ToBeVisibleAsync();
        await Assertions.Expect(InventoryContainer).ToBeVisibleAsync();
    }

    public async Task LogoutAsync() {
        await OpenMenuButton.ClickAsync();
        await LogoutButton.ClickAsync();
    }

    public async Task SortProductsByPriceLowToHigh() {
        await ProductSortMenuButton.SelectOptionAsync(new SelectOptionValue {
            Label = "Price (low to high)"
        });
    }

    public async Task AssertActiveSortOptionTextAsync(string expectedText) {
        await Assertions.Expect(ActiveSortOption).ToHaveTextAsync(expectedText);
    }

    public async Task<List<decimal>> GetProductPricesAsync() {
        var prices = await ProductPrices.AllTextContentsAsync();

        return prices
            .Select(prices => prices.Replace("$", ""))
            .Select(prices => decimal.Parse(prices, CultureInfo.InvariantCulture))
            .ToList();
    }

    public async Task AddProductToCartAsync(string productName) {
        var product = _page
            .Locator("[data-test='inventory-item']")
            .Filter(new() { HasText = productName });

        await product
            .Locator("button")
            .GetByText("Add to cart")
            .ClickAsync();
    }

    public async Task OpenShoppingCart() {
        await shoppingCartButton.ClickAsync();
    }

    public async Task OpenProductDetailsAsync(string productName) {
        await _page.GetByText(productName, new() { Exact = true }).ClickAsync();
    }
}