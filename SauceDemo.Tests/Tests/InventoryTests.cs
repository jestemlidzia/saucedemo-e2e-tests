using FluentAssertions;

using SauceDemo.Tests.TestData;
using SauceDemo.Tests.Fixtures;

namespace SauceDemo.Tests;

public class InventoryTests : AuthenticatedBaseTest {

    [Test]
    public async Task TC10_SortProductsByPriceLowToHigh() {
        await _inventoryPage.SortProductsByPriceLowToHigh();
        await _inventoryPage.AssertActiveSortOptionTextAsync("Price (low to high)");
        var prices = await _inventoryPage.GetProductPricesAsync();

        prices.Should().BeInAscendingOrder();
    }
}
