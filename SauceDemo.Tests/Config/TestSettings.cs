namespace SauceDemo.Tests.Config;

public static class TestSettings {

    public const string BaseUrl = "https://www.saucedemo.com";

    public static readonly string ProjectRootPath = Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));

    public static readonly string ScreenshotsPath =
        Path.Combine(ProjectRootPath, "Artifacts", "Screenshots");

    public static readonly string TracesPath =
        Path.Combine(ProjectRootPath, "Artifacts", "Traces");

}