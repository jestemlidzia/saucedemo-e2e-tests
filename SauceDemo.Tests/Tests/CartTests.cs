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
        await _inventoryPage.AddProductToCartAsync(Products.BikeLight);
        await _inventoryPage.OpenShoppingCart();
        await _cartPage.AssertProductIsInCartAsync(Products.BikeLight);
    }

    [Test]
    public async Task TC06_AddTwoDifferentProductsToCartUsingDifferentMethods() {
        await _inventoryPage.AddProductToCartAsync(Products.FleeceJacket);
        await _inventoryPage.OpenProductDetailsAsync(Products.Backpack);

        await _productDetailsPage.AddProductToCartAsync();
        await _productDetailsPage.OpenShoppingCartAsync();

        await _cartPage.AssertProductIsInCartAsync(Products.FleeceJacket);
        await _cartPage.AssertProductIsInCartAsync(Products.Backpack);
    }

    [Test]
    public async Task TC07_RemoveProductFromCart() {
        await _inventoryPage.AddProductToCartAsync(Products.BikeLight);
        await _inventoryPage.AddProductToCartAsync(Products.Onesie);

        await _inventoryPage.OpenShoppingCart();

        await _cartPage.AssertProductIsInCartAsync(Products.BikeLight);
        await _cartPage.AssertProductIsInCartAsync(Products.Onesie);

        await _cartPage.RemoveProductByNameAsync(Products.BikeLight);

        await _cartPage.AssertProductIsNotInCartAsync(Products.BikeLight);
        await _cartPage.AssertProductIsInCartAsync(Products.Onesie);
    }

    [Test]
    public async Task TC09_CartPricesShouldMatchInventoryPrices() {
        var expectedPrices = new Dictionary<string, decimal> {
            { Products.FleeceJacket, 49.99m },
            { Products.BoltTShirt, 15.99m },
            { Products.BikeLight, 9.99m }
        };

        foreach (var productName in expectedPrices.Keys) {
            await _inventoryPage.AddProductToCartAsync(productName);
        }

        await _inventoryPage.OpenShoppingCart();
        var prices = await _cartPage.GetProductPricesAsync();
        prices.Should().BeEquivalentTo(expectedPrices);
    }

}
