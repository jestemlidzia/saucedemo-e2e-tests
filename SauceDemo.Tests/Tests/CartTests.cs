using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

using SauceDemo.Tests.Config;
using SauceDemo.Tests.Pages;
using SauceDemo.Tests.TestData;
using SauceDemo.Tests.Fixtures;

namespace SauceDemo.Tests;

public class CartTest : AuthenticatedBaseTest {

    [Test]
    public async Task TC05_AddSingleProductToCart() {
        await _inventoryPage.AddProductToCartAsync("Sauce Labs Bike Light");
        // check chart
        // _cartPage
    }

    [Test]
    public async Task TC06_AddTwoDifferentProductsToCartUsingDifferentMethods() {
        // ...
    }

    [Test]
    public async Task TC07_RemoveProductFromCart() {
        // ...
    }

    [Test]
    public async Task TC09_CartPricesShouldMatchInventoryPrices() {
        // ...
    }

}
