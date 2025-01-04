namespace Boa.Constrictor.Playwright
{
    using System.Threading.Tasks;
    using Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Hovers cursor over a web element.
    /// </summary>
    public class Hover : AbstractLocatorTask
    {
        private readonly LocatorHoverOptions Options;

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="locator">The target locator.</param>
        /// <param name="options">Call options.</param>
        private Hover(IPlaywrightLocator locator, LocatorHoverOptions options)
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
        public static Hover On(IPlaywrightLocator locator, LocatorHoverOptions options = null) => new Hover(locator, options);

        #endregion

        #region Methods

        /// <summary>
        /// Hovers cursor over the web element.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="locator">The target locator.</param>
        public override async Task PerformAsAsync(IActor actor, ILocator locator)
        {
            await locator.HoverAsync(Options);
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"hover over {Locator.Description}";

        #endregion
    }
}