namespace Boa.Constrictor.Playwright
{
    using System.Threading.Tasks;
    using Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Press a combination of keys
    /// </summary>
    public class Press : AbstractLocatorTask
    {
        private string Key { get; }
        private readonly LocatorPressOptions Options;

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="locator">The target locator.</param>
        /// <param name="key">Name of the key to press or a character to generate.</param>
        /// <param name="options">Call options.</param>
        private Press(IPlaywrightLocator locator, string key, LocatorPressOptions options)
            :base(locator)
        {
            Key = key;
            Options = options;
        }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Task object.
        /// </summary>
        /// <param name="locator">The target locator.</param>
        /// <param name="key">Name of the key to press or a character to generate, such as 'ArrowLeft' or 'a'</param>
        /// <param name="options">Call options.</param>
        /// <returns></returns>
        public static Press KeyOn(IPlaywrightLocator locator, string key, LocatorPressOptions options = null) => new Press(locator, key, options);

        #endregion

        #region Methods

        /// <summary>
        /// Press a combination of keys on the web element.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="locator">The target locator.</param>
        public override async Task PerformAsAsync(IActor actor, ILocator locator)
        {
            await locator.PressAsync(Key, Options);
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"press the {Key} key on {Locator.Description}";

        #endregion
    }
}