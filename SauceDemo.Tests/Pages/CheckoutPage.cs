using Microsoft.Playwright;
using System.Globalization;

namespace SauceDemo.Tests.Pages;

public class CheckoutPage {

    private readonly IPage _page;

    public CheckoutPage(IPage page) {
        _page = page;
    }

    private ILocator FirstNameInput => _page.GetByPlaceholder("First Name");
    private ILocator LastNameInput => _page.GetByPlaceholder("Last Name");
    private ILocator PostalCodeInput => _page.GetByPlaceholder("Zip/Postal Code");
    private ILocator ContinueButton => _page.Locator("[data-test='continue']");
    private ILocator FinishButton => _page.Locator("[data-test='finish']");
    private ILocator CompleteHeader => _page.Locator("[data-test='complete-header']");
    private ILocator ItemTotalLabel => _page.Locator("[data-test='subtotal-label']");

    public async Task FillCustomerInformationAndContinueAsync(
        string firstName,
        string lastName,
        string postalCode) {
            await FirstNameInput.FillAsync(firstName);
            await LastNameInput.FillAsync(lastName);
            await PostalCodeInput.FillAsync(postalCode);
            await ContinueButton.ClickAsync();
    }

    public async Task FinishOrderAsync() {
        await FinishButton.ClickAsync();
    }

    public async Task AssertOrderCompletedAsync(string expectedMessage) {
        await Assertions.Expect(CompleteHeader).ToHaveTextAsync(expectedMessage);
    }

    public async Task AssertItemTotalAsync(decimal expectedTotal) {
        var formattedTotal = expectedTotal.ToString("F2", CultureInfo.InvariantCulture);
        var expectedText = $"Item total: ${formattedTotal}";

        await Assertions.Expect(ItemTotalLabel).ToHaveTextAsync(expectedText);
    }
}