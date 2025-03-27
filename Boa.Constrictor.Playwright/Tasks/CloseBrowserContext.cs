using System.Threading.Tasks;
using Boa.Constrictor.Screenplay;

namespace Boa.Constrictor.Playwright
{
    /// <summary>
    /// Closes the current browser context
    /// </summary>
    public class CloseBrowserContext : ITaskAsync
    {
        #region Builder Methods

        /// <summary>
        /// Constructs the Task object
        /// </summary>
        /// <returns></returns>
        public static CloseBrowserContext Instance() => new CloseBrowserContext();

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
            browseTheWeb.BrowserContext = null;
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "close the browser context";
        }

        #endregion
    }
}