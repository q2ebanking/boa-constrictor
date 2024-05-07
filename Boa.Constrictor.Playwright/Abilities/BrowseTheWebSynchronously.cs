namespace Boa.Constrictor.Playwright.Abilities
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boa.Constrictor.Screenplay;
    using Microsoft.Playwright;

    /// <summary>
/// Enables the Actor to use a Web browser via Playwright
/// </summary>
public class BrowseTheWebSynchronously : IAbility
{
    private IBrowserContext currentContext;

    /// <summary>
    /// Private constructor.
    /// (Use the static methods for public construction.)
    /// </summary>
    /// <param name="playwright">The Playwright instance</param>
    /// <param name="browser">The Playwright browser instance</param>
    private BrowseTheWebSynchronously(IPlaywright playwright, IBrowser browser)
    {
        Playwright = playwright;
        Browser = browser;
        Pages = new List<IPage>();
    }

    public IBrowser Browser { get; }
    public IPage CurrentPage { get; set; }
    public IList<IPage> Pages { get; }
    public IPlaywright Playwright { get; }

    /// <summary>
    /// Supply a pre-defined Plawright browser to use
    /// </summary>
    /// <param name="playwright">The Playwright instance</param>
    /// <param name="browser">The Playwright browser instance.</param>
    /// <returns>An instance of <see cref="BrowseTheWebSynchronously"/></returns>
    public static BrowseTheWebSynchronously Using(IPlaywright playwright, IBrowser browser)
    {
        return new BrowseTheWebSynchronously(playwright, browser);
    }

    /// <summary>
    /// Use a synchronous Chromium (i.e. Chrome, Edge, Opera, etc.) browser.
    /// </summary>
    /// <returns>An instance of <see cref="BrowseTheWebSynchronously"/> configured to use Chromium</returns>
    public static async Task<BrowseTheWebSynchronously> UsingChromium(BrowserTypeLaunchOptions options = null)
    {
        var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(options);
        return new BrowseTheWebSynchronously(playwright, browser);
    }

    /// <summary>
    /// Use a synchronous Firefox browser
    /// </summary>
    /// <returns>An instance of <see cref="BrowseTheWebSynchronously"/> configured to use firefox</returns>
    public static async Task<BrowseTheWebSynchronously> UsingFirefox(BrowserTypeLaunchOptions options = null)
    {
        var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        var browser = await playwright.Firefox.LaunchAsync(options);
        return new BrowseTheWebSynchronously(playwright, browser);
    }
    /// <summary>
    /// Use a synchronous WebKit (i.e. Safari, etc.) browser.
    /// </summary>
    /// <returns>An instance of <see cref="BrowseTheWebSynchronously"/> configured to use Webkit</returns>
    public static async Task<BrowseTheWebSynchronously> UsingWebkit(BrowserTypeLaunchOptions options = null)
    {
        var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        var browser = await playwright.Webkit.LaunchAsync(options);
        return new BrowseTheWebSynchronously(playwright, browser);
    }

    public async Task<IPage> CurrentPageAsync()
    {
        if (CurrentPage == null)
        {
            var context = await GetBrowserContextAsync();
            CurrentPage = await context.NewPageAsync();
        }

        return CurrentPage;
    }

    /// <summary>
    /// Returns a description of this Ability
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"browse the web with playwright using {Browser.BrowserType.Name}";
    }

    private async Task<IBrowserContext> GetBrowserContextAsync()
    {
        if (currentContext == null)
        {
            currentContext = await Browser.NewContextAsync();
        }

        return currentContext;
    }
}
}