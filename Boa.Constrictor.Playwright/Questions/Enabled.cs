using System.Threading.Tasks;
using Boa.Constrictor.Screenplay;
using Microsoft.Playwright;

namespace Boa.Constrictor.Playwright
{
    /// <summary>
    /// Gets whether the element is enabled
    /// </summary>
    public class Enabled : AbstractLocatorQuestion<bool>
    {
        #region Constructor

        private Enabled(IPlaywrightLocator locator, LocatorIsEnabledOptions options) : base(locator)
        {
            Options = options;
        }

        #endregion

        #region Properties

        private LocatorIsEnabledOptions Options { get; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target element.</param>
        /// <param name="options">Call options</param>
        /// 
        public static Enabled Of(IPlaywrightLocator locator, LocatorIsEnabledOptions options = null)
        {
            return new Enabled(locator, options);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets whether element is enabled
        /// </summary>
        /// <param name="actor">The actor</param>
        /// <param name="locator">The target element</param>
        /// <returns></returns>
        public override async Task<bool> RequestAsAsync(IActor actor, ILocator locator)
        {
            return await locator.IsEnabledAsync(Options);
        }

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"enabled of {Locator.Description}";
        }

        #endregion
    }
}