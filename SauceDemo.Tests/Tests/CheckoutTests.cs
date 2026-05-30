using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

using SauceDemo.Tests.Config;
using SauceDemo.Tests.Pages;
using SauceDemo.Tests.TestData;
using SauceDemo.Tests.Fixtures;

namespace SauceDemo.Tests;

public class CheckoutTest : AuthenticatedBaseTest {

    [Test]
    public async Task TC08_CompleteFullPurchaseProcess() {
        await _inventoryPage.AddProductToCartAsync("Sauce Labs Bike Light");
        await _inventoryPage.AddProductToCartAsync("Sauce Labs Bolt T-Shirt");
        await _inventoryPage.OpenShoppingCart();
        await _cartPage.AssertProductIsInCartAsync("Sauce Labs Bike Light");
        await _cartPage.AssertProductIsInCartAsync("Sauce Labs Bolt T-Shirt");
        await _cartPage.GoToCheckoutAsync();

        await _checkoutPage.FillCustomerInformationAndContinueAsync(
            "Anna", "Smith", "33-353"
        );

        await _checkoutPage.AssertItemTotalAsync(25.98m);
        await _checkoutPage.FinishOrderAsync();
        await _checkoutPage.AssertOrderCompletedAsync("Thank you for your order!");
    }
}
