namespace Boa.Constrictor.Playwright
{
    using System.Threading.Tasks;
    using Boa.Constrictor.Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Focuses the element, and then sends a <c>keydown</c>, <c>keypress</c>/<c>input</c>,
    /// and <c>keyup</c> event for each character in the text.
    /// </summary>
    /// <remarks>
    /// In most cases, you should use <see cref="Fill"/> instead. You only
    /// need to press keys one by one if there is special keyboard handling on the page.
    /// </remarks>
    public class PressSequentially : AbstractLocatorTask
    {
        #region Constructors

        private PressSequentially(IPlaywrightLocator locator, string text, LocatorPressSequentiallyOptions options) : base(locator)
        {
            Text = text;
            Options = options;
        }

        #endregion

        #region Properties

        private string Text { get; }
        private LocatorPressSequentiallyOptions Options { get; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Focuses the element, and then sends a <c>keydown</c>, <c>keypress</c>/<c>input</c>,
        /// and <c>keyup</c> event for each character in the text.
        /// </summary>
        /// <remarks>
        /// In most cases, you should use <see cref="Fill"/> instead. You only
        /// need to press keys one by one if there is special keyboard handling on the page.
        /// </remarks>
        /// <param name="locator">The target locator.</param>
        /// <param name="text">String of characters to sequentially press into a focused element.</param>
        /// <param name="options">Call options.</param>
        public static PressSequentially On(IPlaywrightLocator locator, string text,
            LocatorPressSequentiallyOptions options = null)
        {
            return new PressSequentially(locator, text, options);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Focuses the element, and then sends a <c>keydown</c>, <c>keypress</c>/<c>input</c>,
        /// and <c>keyup</c> event for each character in the text.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <param name="locator">The target locator.</param>
        public override async Task PerformAsAsync(IActor actor, ILocator locator)
        {
            await locator.PressSequentiallyAsync(Text, Options);
        }

        #endregion
    }
}