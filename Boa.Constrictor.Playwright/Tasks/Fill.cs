namespace Boa.Constrictor.Playwright
{
    using System.Threading.Tasks;
    using Boa.Constrictor.Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Inputs text in an element
    /// </summary>
    public class Fill : AbstractLocatorTask
    {
        private readonly string Value;
        private readonly LocatorFillOptions Options;

        #region Constructors

        private Fill(IPlaywrightLocator locator, string value, LocatorFillOptions options)
            :base(locator)
        {
            Value = value;
            Options = options;
        }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Task object.
        /// </summary>
        /// <param name="locator">The target locator.</param>
        /// <param name="value">The value to be filled(e.g., text).</param>
        /// <param name="options">Call options.</param>
        /// <returns></returns>
        public static Fill ValueTo(IPlaywrightLocator locator, string value, LocatorFillOptions options = null) => new Fill(locator, value, options);

        #endregion

        #region Methods

        /// <summary>
        /// Inputs text in an element
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="locator">The target locator.</param>
        public override async Task PerformAsAsync(IActor actor, ILocator locator)
        {
            await locator.FillAsync(this.Value, Options);
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"fill the text '{Value}' into {Locator.Description}";
        }

        #endregion
    }
}