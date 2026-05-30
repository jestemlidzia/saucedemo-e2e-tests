using Microsoft.Playwright;

namespace SauceDemo.Tests.Pages;

public class CartPage {

    private readonly IPage _page;

    public CartPage(IPage page) {
        _page = page;
    }

}