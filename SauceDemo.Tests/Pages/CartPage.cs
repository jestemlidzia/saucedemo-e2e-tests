using Microsoft.Playwright;
using System.Globalization;

namespace SauceDemo.Tests.Pages;

public class CartPage {

    private readonly IPage _page;

    public CartPage(IPage page) {
        _page = page;
    }

    private ILocator CartItems => _page.Locator("[data-test='inventory-item']");
    private ILocator CartItemByName(string productName) => CartItems.Filter(new() { HasText = productName });
    private ILocator CheckoutButton => _page.GetByText("Checkout");

    public async Task AssertProductIsInCartAsync(string productName) {
        await Assertions.Expect(CartItemByName(productName)).ToBeVisibleAsync();
    }

    public async Task AssertProductIsNotInCartAsync(string productName) {
        await Assertions.Expect(CartItemByName(productName)).Not.ToBeVisibleAsync();
    }

    public async Task RemoveProductByNameAsync(string productName) {
        var cartItem = CartItemByName(productName);

        await cartItem
            .Locator("button")
            .GetByText("Remove")
            .ClickAsync();
    }

    public async Task<Dictionary<string, decimal>> GetProductPricesAsync() {
        var result = new Dictionary<string, decimal>();
        var items = await CartItems.AllAsync();

        foreach (var item in items) {
            var name = await item
                .Locator("[data-test='inventory-item-name']")
                .TextContentAsync();

            var priceText = await item
                .Locator("[data-test='inventory-item-price']")
                .TextContentAsync();

            var price = decimal.Parse(
                priceText!.Replace("$", ""),
                CultureInfo.InvariantCulture);

            result.Add(name!, price);
        }

        return result;
    }

    public async Task GoToCheckoutAsync() {
        await CheckoutButton.ClickAsync();
    }
}