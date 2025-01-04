namespace Boa.Constrictor.Playwright
{
    using System.Threading.Tasks;
    using Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Double-clicks a web element.
    /// </summary>
    public class DblClick : AbstractLocatorTask
    {
        private readonly LocatorDblClickOptions Options;

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="locator">The target locator.</param>
        /// <param name="options">Call options.</param>
        private DblClick(IPlaywrightLocator locator, LocatorDblClickOptions options)
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
        public static DblClick On(IPlaywrightLocator locator, LocatorDblClickOptions options = null) => new DblClick(locator, options);

        #endregion

        #region Methods

        /// <summary>
        /// Double-clicks the web element.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="locator">The target locator.</param>
        public override async Task PerformAsAsync(IActor actor, ILocator locator)
        {
            await locator.DblClickAsync(Options);
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"double-click on {Locator.Description}";

        #endregion
    }
}