namespace Boa.Constrictor.Playwright
{
    using System.Threading.Tasks;
    using Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Focus on a web element.
    /// </summary>
    public class Focus : AbstractLocatorTask
    {
        private readonly LocatorFocusOptions Options;

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="locator">The target locator.</param>
        /// <param name="options">Call options.</param>
        private Focus(IPlaywrightLocator locator, LocatorFocusOptions options)
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
        public static Focus On(IPlaywrightLocator locator, LocatorFocusOptions options = null) => new Focus(locator, options);

        #endregion

        #region Methods

        /// <summary>
        /// Focus on the web element.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="locator">The target locator.</param>
        public override async Task PerformAsAsync(IActor actor, ILocator locator)
        {
            await locator.FocusAsync(Options);
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"click on {Locator.Description}";

        #endregion
    }
}