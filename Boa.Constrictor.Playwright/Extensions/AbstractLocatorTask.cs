namespace Boa.Constrictor.Playwright
{
    using System.Threading.Tasks;
    using Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Abstract class that makes it easier to write tasks that perform operations on a single locator.
    /// </summary>
    public abstract class AbstractLocatorTask : ITaskAsync
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="locator">The locator.</param>
        public AbstractLocatorTask(IPlaywrightLocator locator)
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
        /// Performs the Task.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="locator">The locator to perform the task on.</param>
        public abstract Task PerformAsAsync(IActor actor, ILocator locator);

        #endregion

        #region Methods

        /// <summary>
        /// Performs the Task.
        /// Internally calls PerformAs with the locator found on the CurrentPage from the BrowseTheWebWithPlaywright Ability.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        public virtual async Task PerformAsAsync(IActor actor)
        {
            var page = await actor.Using<BrowseTheWebWithPlaywright>().GetCurrentPageAsync();
            await PerformAsAsync(actor, Locator.FindIn(page));
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => GetType().Name;

        #endregion
    }
}