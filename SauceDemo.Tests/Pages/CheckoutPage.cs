using Microsoft.Playwright;

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
}