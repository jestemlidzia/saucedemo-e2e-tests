using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using FluentAssertions;

using SauceDemo.Tests.Config;
using SauceDemo.Tests.Pages;
using SauceDemo.Tests.TestData;
using SauceDemo.Tests.Fixtures;

namespace SauceDemo.Tests;

public class InventoryTest : AuthenticatedBaseTest {

    [Test]
    public async Task TC10_SortProductsByPriceLowToHigh() {
        await _inventoryPage.SortProductsByPriceLowToHigh();
        await _inventoryPage.AssertActiveSortOptionTextAsync("Price (low to high)");
        var prices = await _inventoryPage.GetProductPricesAsync();

        prices.Should().BeInAscendingOrder();
    }
}
