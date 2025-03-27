using System.Threading.Tasks;
using Boa.Constrictor.Screenplay;

namespace Boa.Constrictor.Playwright
{
    /// <summary>
    /// Closes the browser and all of its pages
    /// </summary>
    public class CloseBrowser : ITaskAsync
    {
        #region Builder Methods

        /// <summary>
        /// Constructs the Task object
        /// </summary>
        /// <returns></returns>
        public static CloseBrowser Instance() => new CloseBrowser();

        #endregion
        
        #region Methods

        /// <summary>
        /// Closes the current browser context, then closes the browser
        /// </summary>
        /// <param name="actor">The actor</param>
        /// <returns></returns>
        public async Task PerformAsAsync(IActor actor)
        {
            var browseTheWeb = actor.Using<BrowseTheWebWithPlaywright>();
            var context = await browseTheWeb.GetBrowserContextAsync();
            
            await context.CloseAsync();
            await browseTheWeb.Browser.CloseAsync();
            
            browseTheWeb.BrowserContext = null;
            browseTheWeb.Browser = null;
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "close the browser";
        }

        #endregion
    }
}