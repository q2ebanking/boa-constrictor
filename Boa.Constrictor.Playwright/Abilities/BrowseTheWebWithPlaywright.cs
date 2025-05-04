using System.Collections.Generic;
using System.Threading.Tasks;
using Boa.Constrictor.Screenplay;
using Microsoft.Playwright;

namespace Boa.Constrictor.Playwright
{
    /// <summary>
    ///     Enables the Actor to use a Web browser via Playwright
    /// </summary>
    public class BrowseTheWebWithPlaywright : IAbility
    {
        /// <summary>
        ///     Private constructor.
        ///     (Use the static methods for public construction.)
        /// </summary>
        /// <param name="playwright">The Playwright instance</param>
        /// <param name="browser">The Playwright browser instance</param>
        private BrowseTheWebWithPlaywright(IPlaywright playwright, IBrowser browser)
        {
            Playwright = playwright;
            Browser = browser;
            Pages = new List<IPage>();
        }

        /// <summary>
        /// The <see cref="IPlaywright"/> instance
        /// </summary>
        public IPlaywright Playwright { get; }
        
        /// <summary>
        /// The <see cref="IBrowser"/> instance
        /// </summary>
        public IBrowser Browser { get; set; }
        
        /// <summary>
        /// A collection of open pages
        /// </summary>
        public IList<IPage> Pages { get; }
        
        /// <summary>
        /// The currently active page
        /// </summary>
        public  IPage CurrentPage { get; set; }

        /// <summary>
        /// The browser context
        /// </summary>
        public IBrowserContext BrowserContext { get; set; }


        /// <summary>
        ///     Supply a pre-defined Plawright browser to use
        /// </summary>
        /// <param name="playwright">The Playwright instance</param>
        /// <param name="browser">The Playwright browser instance.</param>
        /// <returns>An instance of <see cref="BrowseTheWebWithPlaywright" /></returns>
        public static BrowseTheWebWithPlaywright Using(IPlaywright playwright, IBrowser browser)
        {
            return new BrowseTheWebWithPlaywright(playwright, browser);
        }

        /// <summary>
        ///     Use a Chromium (i.e. Chrome, Edge, Opera, etc.) browser.
        /// </summary>
        /// <returns>An instance of <see cref="BrowseTheWebWithPlaywright" /> configured to use Chromium</returns>
        public static async Task<BrowseTheWebWithPlaywright> UsingChromium(BrowserTypeLaunchOptions options = null)
        {
            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(options);
            return new BrowseTheWebWithPlaywright(playwright, browser);
        }

        /// <summary>
        ///     Use a Firefox browser
        /// </summary>
        /// <returns>An instance of <see cref="BrowseTheWebWithPlaywright" /> configured to use firefox</returns>
        public static async Task<BrowseTheWebWithPlaywright> UsingFirefox(BrowserTypeLaunchOptions options = null)
        {
            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            var browser = await playwright.Firefox.LaunchAsync(options);
            return new BrowseTheWebWithPlaywright(playwright, browser);
        }

        /// <summary>
        ///     Use a WebKit (i.e. Safari, etc.) browser.
        /// </summary>
        /// <returns>An instance of <see cref="BrowseTheWebWithPlaywright" /> configured to use Webkit</returns>
        public static async Task<BrowseTheWebWithPlaywright> UsingWebkit(BrowserTypeLaunchOptions options = null)
        {
            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            var browser = await playwright.Webkit.LaunchAsync(options);
            return new BrowseTheWebWithPlaywright(playwright, browser);
        }

        /// <summary>
        /// Gets the current page. If no page exists, a new page is created.
        /// </summary>
        /// <returns>The current page.</returns>
        public async Task<IPage> GetCurrentPageAsync()
        {
            if (CurrentPage == null)
            {
                var context = await GetBrowserContextAsync();
                CurrentPage = await context.NewPageAsync();
                Pages.Add(CurrentPage);
            }

            return CurrentPage;
        }
        
        /// <summary>
        /// Gets the current browser context. If no context exists, a new one is created.
        /// </summary>
        /// <returns>The current browser context.</returns>
        public async Task<IBrowserContext> GetBrowserContextAsync()
        {
            if (BrowserContext == null)
            {
                BrowserContext = await Browser.NewContextAsync();
            }

            return BrowserContext;
        }

        /// <summary>
        ///     Returns a description of this Ability
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"browse the web with playwright using {Browser.BrowserType.Name}";
        }
    }
}