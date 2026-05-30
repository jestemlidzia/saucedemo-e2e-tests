namespace SauceDemo.Tests.TestData;

public static class LoginTestData {

    public const string StandardUser = "standard_user";
    public const string LockedOutUser = "locked_out_user";
    public const string NotExistingUser = "not_existing_user";
    public const string Password = "secret_sauce";

    public const string InvalidCredentialsError = 
        "Epic sadface: Username and password do not match any user in this service";
    
    public const string LockedOutUserError = 
        "Epic sadface: Sorry, this user has been locked out.";
}