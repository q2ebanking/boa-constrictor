namespace Boa.Constrictor.Playwright
{
    using System.Threading.Tasks;
    using Boa.Constrictor.Playwright.Abilities;
    using Boa.Constrictor.Screenplay;

    /// <summary>
    /// Opens a new page to the specified url
    /// </summary>
    public class OpenNewPage : ITaskAsync
    {
        private readonly string Url;

        #region Constructors

        private OpenNewPage(string url)
        {
            this.Url = url;
        }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Opens a new page to the specified url
        /// </summary>
        /// <param name="url">The url the new page should be opened to</param>
        /// <returns></returns>
        public static OpenNewPage ToUrl(string url)
        {
            return new OpenNewPage(url);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Opens a new page to the specified URL.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        public async Task PerformAsAsync(IActor actor)
        {
            var browseTheWeb = actor.Using<BrowseTheWebWithPlaywright>();
            var page = await browseTheWeb.Browser.NewPageAsync();
            await page.GotoAsync(Url);
            browseTheWeb.CurrentPage = page;
            browseTheWeb.Pages.Add(page);
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"open a new page to {Url}";

        #endregion

    }
}