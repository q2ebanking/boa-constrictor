namespace Boa.Constrictor.Playwright
{
    using System.Threading.Tasks;
    using Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Clear the input field.
    /// </summary>
    public class Clear : AbstractLocatorTask
    {
        private readonly LocatorClearOptions Options;

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="locator">The target locator.</param>
        /// <param name="options">Call options.</param>
        private Clear(IPlaywrightLocator locator, LocatorClearOptions options)
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
        public static Clear On(IPlaywrightLocator locator, LocatorClearOptions options = null) => new Clear(locator, options);

        #endregion

        #region Methods

        /// <summary>
        /// Clicks the input field.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="locator">The target locator.</param>
        public override async Task PerformAsAsync(IActor actor, ILocator locator)
        {
            await locator.ClearAsync(Options);
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"clear text in {Locator.Description}";

        #endregion
    }
}