using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using FluentAssertions;

using SauceDemo.Tests.Config;
using SauceDemo.Tests.Pages;
using SauceDemo.Tests.TestData;
using SauceDemo.Tests.Fixtures;

namespace SauceDemo.Tests;

public class CartTest : AuthenticatedBaseTest {

    [Test]
    public async Task TC05_AddSingleProductToCart() {
        await _inventoryPage.AddProductToCartAsync("Sauce Labs Bike Light");
        await _inventoryPage.OpenShoppingCart();
        await _cartPage.AssertProductIsInCartAsync("Sauce Labs Bike Light");
    }

    [Test]
    public async Task TC06_AddTwoDifferentProductsToCartUsingDifferentMethods() {
        // ...
    }

    [Test]
    public async Task TC07_RemoveProductFromCart() {
        await _inventoryPage.AddProductToCartAsync("Sauce Labs Bike Light");
        await _inventoryPage.AddProductToCartAsync("Sauce Labs Onesie");

        await _inventoryPage.OpenShoppingCart();

        await _cartPage.AssertProductIsInCartAsync("Sauce Labs Bike Light");
        await _cartPage.AssertProductIsInCartAsync("Sauce Labs Onesie");

        await _cartPage.RemoveProductByNameAsync("Sauce Labs Bike Light");

        await _cartPage.AssertProductIsNotInCartAsync("Sauce Labs Bike Light");
        await _cartPage.AssertProductIsInCartAsync("Sauce Labs Onesie");
    }

    [Test]
    public async Task TC09_CartPricesShouldMatchInventoryPrices() {
        var expectedPrices = new Dictionary<string, decimal> {
            { "Sauce Labs Fleece Jacket", 49.99m },
            { "Sauce Labs Bolt T-Shirt", 15.99m },
            { "Sauce Labs Bike Light", 9.99m }
        };

        foreach (var productName in expectedPrices.Keys) {
            await _inventoryPage.AddProductToCartAsync(productName);
        }

        await _inventoryPage.OpenShoppingCart();
        var prices = await _cartPage.GetProductPricesAsync();
        prices.Should().BeEquivalentTo(expectedPrices);
    }

}
