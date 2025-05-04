using System.Threading.Tasks;
using Boa.Constrictor.Screenplay;
using Microsoft.Playwright;

namespace Boa.Constrictor.Playwright
{
    /// <summary>
    /// Gets the tile of the current page
    /// </summary>
    public class PageTitle : AbstractPageQuestion<string>
    {
        #region Builder Methods
        
        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <returns></returns>
        public static PageTitle OfPage() => new PageTitle();
        
        #endregion
        
        #region Methods
        
        /// <summary>
        /// Gets the title of the current page
        /// </summary>
        /// <returns></returns>
        public override async Task<string> RequestAsAsync(IActor actor, IPage page)
        {
            return await page.TitleAsync();
        }

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "page title";
        }
        
        #endregion
    }
}