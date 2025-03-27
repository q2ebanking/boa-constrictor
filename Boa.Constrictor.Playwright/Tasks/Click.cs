namespace Boa.Constrictor.Playwright
{
    using System.Threading.Tasks;
    using Boa.Constrictor.Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Clicks a web element.
    /// </summary>
    public class Click : AbstractLocatorTask
    {
        private readonly LocatorClickOptions Options;

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="locator">The target locator.</param>
        /// <param name="options">Call options.</param>
        private Click(IPlaywrightLocator locator, LocatorClickOptions options)
            :base(locator)
        {
            Options = options;
        }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Task object.
        /// </summary>
        /// <param name="locator">The target locator.</param>
        /// <param name="options">Call options.</param>
        /// <returns></returns>
        public static Click On(IPlaywrightLocator locator, LocatorClickOptions options = null) => new Click(locator, options);

        #endregion

        #region Methods

        /// <summary>
        /// Clicks the web element.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="locator">The target locator.</param>
        public override async Task PerformAsAsync(IActor actor, ILocator locator)
        {
            await locator.ClickAsync(Options);
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"click on {Locator.Description}";

        #endregion
    }
}