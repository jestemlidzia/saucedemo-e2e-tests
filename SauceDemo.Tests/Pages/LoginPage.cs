using Microsoft.Playwright;

namespace SauceDemo.Tests.Pages;

public class LoginPage {

    private readonly IPage _page;

    public LoginPage(IPage page) {
        _page = page;
    }

    private ILocator UsernameInput => _page.GetByPlaceholder("Username");
    private ILocator PasswordInput => _page.GetByPlaceholder("Password");
    private ILocator LoginButton => _page.GetByText("Login");
    private ILocator ErrorMessage => _page.Locator("[data-test='error']");

    public async Task LoginAsync(string username, string password) {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
    }
    
    public async Task AssertErrorMessageAsync(string expectedErrorMessage) {
        await Assertions.Expect(ErrorMessage).ToHaveTextAsync(expectedErrorMessage);
    }

    public async Task AssertLoginPageVisibleAsync() {
        await Assertions.Expect(UsernameInput).ToBeVisibleAsync();
        await Assertions.Expect(PasswordInput).ToBeVisibleAsync();
        await Assertions.Expect(LoginButton).ToBeVisibleAsync();
    }
}