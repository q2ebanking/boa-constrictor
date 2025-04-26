using System.Collections.Concurrent;
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
        
        private static readonly ConcurrentDictionary<IBrowserContext, BrowseTheWebWithPlaywright> _browserContextRegistry = 
            new ConcurrentDictionary<IBrowserContext, BrowseTheWebWithPlaywright>();
            
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
        /// The currently active frame
        /// </summary>
        public IFrame CurrentFrame { get; set; }

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
        /// Gets the current frame. If no frame is set, returns the main frame of the current page.
        /// </summary>
        /// <returns>The current frame.</returns>
        public async Task<IFrame> GetCurrentFrameAsync()
        {
            var page = await GetCurrentPageAsync();
            
            if (CurrentFrame == null)
            {
                CurrentFrame = page.MainFrame;
            }
            
            return CurrentFrame;
        }
        
        /// <summary>
        /// Sets the current frame to the main frame of the current page.
        /// </summary>
        /// <returns>The main frame that was set as current.</returns>
        public async Task<IFrame> SetCurrentFrameToMainAsync()
        {
            var page = await GetCurrentPageAsync();
            CurrentFrame = page.MainFrame;
            return CurrentFrame;
        }
        
        /// <summary>
        /// Sets the current frame by name.
        /// </summary>
        /// <param name="name">The name of the frame to set as current.</param>
        /// <returns>The frame that was set as current, or null if not found.</returns>
        public async Task<IFrame> SetCurrentFrameByNameAsync(string name)
        {
            var page = await GetCurrentPageAsync();
            var frame = page.Frame(name);
            if (frame != null)
            {
                CurrentFrame = frame;
            }
            return frame;
        }
        
        /// <summary>
        /// Sets the current frame by URL.
        /// </summary>
        /// <param name="url">The URL of the frame to set as current.</param>
        /// <returns>The frame that was set as current, or null if not found.</returns>
        public async Task<IFrame> SetCurrentFrameByUrlAsync(string url)
        {
            var page = await GetCurrentPageAsync();
            var frame = page.FrameByUrl(url);
            if (frame != null)
            {
                CurrentFrame = frame;
            }
            return frame;
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
                // Register this ability with the new context
                _browserContextRegistry[BrowserContext] = this;
            }

            return BrowserContext;
        }
        
        /// <summary>
        /// Gets the BrowseTheWebWithPlaywright ability associated with a browser context
        /// </summary>
        /// <param name="context">The browser context</param>
        /// <returns>The associated ability, or null if not found</returns>
        public static BrowseTheWebWithPlaywright GetForContext(IBrowserContext context)
        {
            _browserContextRegistry.TryGetValue(context, out var ability);
            return ability;
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