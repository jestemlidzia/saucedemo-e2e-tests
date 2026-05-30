using Microsoft.Playwright;

namespace SauceDemo.Tests.Pages;

public class ProductDetailsPage {

    private readonly IPage _page;

    public ProductDetailsPage(IPage page) {
        _page = page;
    }

    private ILocator AddToCartButton => _page.GetByText("Add to cart");
    private ILocator BackToInventoryButton => _page.GetByText("Back to products");

    public async Task AddProductToCartAsync()
    {
        await AddToCartButton.ClickAsync();
    }

    public async Task OpenShoppingCartAsync()
    {
        await BackToInventoryButton.ClickAsync();
    }
}