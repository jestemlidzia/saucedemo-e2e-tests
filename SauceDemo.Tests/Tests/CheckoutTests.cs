using SauceDemo.Tests.TestData;
using SauceDemo.Tests.Fixtures;

namespace SauceDemo.Tests;

public class CheckoutTests : AuthenticatedBaseTest {

    [Test]
    public async Task TC08_CompleteFullPurchaseProcess() {
        await _inventoryPage.AddProductToCartAsync(Products.BikeLight);
        await _inventoryPage.AddProductToCartAsync(Products.BoltTShirt);
        await _inventoryPage.OpenShoppingCart();
        await _cartPage.AssertProductIsInCartAsync(Products.BikeLight);
        await _cartPage.AssertProductIsInCartAsync(Products.BoltTShirt);
        await _cartPage.GoToCheckoutAsync();

        await _checkoutPage.FillCustomerInformationAndContinueAsync(
            "Anna", "Smith", "33-353"
        );

        await _checkoutPage.AssertItemTotalAsync(25.98m);
        await _checkoutPage.FinishOrderAsync();
        await _checkoutPage.AssertOrderCompletedAsync("Thank you for your order!");
    }
}
