using System.Threading.Tasks;
using Boa.Constrictor.Screenplay;
using Microsoft.Playwright;

namespace Boa.Constrictor.Playwright
{
    /// <summary>
    /// Abstract class that makes it easier to write questions that perform operations on a single locator.
    /// </summary>
    public abstract class AbstractLocatorQuestion<TAnswer> : IQuestionAsync<TAnswer>
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="locator">The locator.</param>
        public AbstractLocatorQuestion(IPlaywrightLocator locator)
        {
            Locator = locator;
        }
        #endregion
        
        #region Properties

        /// <summary>
        /// The locator.
        /// </summary>
        public IPlaywrightLocator Locator { get; set; }

        #endregion
        
        #region Abstract Methods
        
        /// <summary>
        /// Asks the Question and returns the answer.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="locator">The locator to evaluate.</param>
        /// <returns></returns>
        public abstract Task<TAnswer> RequestAsAsync(IActor actor, ILocator locator);
        
        #endregion
        
        #region Methods
        
        /// <summary>
        /// Asks the Question and returns the answer.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        public virtual async Task<TAnswer> RequestAsAsync(IActor actor)
        {
            var currentPage = await actor.Using<BrowseTheWebWithPlaywright>().GetCurrentPageAsync();
            return await RequestAsAsync(actor, Locator.FindIn(currentPage));
        }
        
        #endregion
    }
}