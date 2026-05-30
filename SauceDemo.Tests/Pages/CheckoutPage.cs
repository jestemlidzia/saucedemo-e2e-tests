using Microsoft.Playwright;

namespace SauceDemo.Tests.Pages;

public class CheckoutPage {

    private readonly IPage _page;

    public CheckoutPage(IPage page) {
        _page = page;
    }

}