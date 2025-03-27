using System.Threading.Tasks;
using Boa.Constrictor.Screenplay;
using Microsoft.Playwright;

namespace Boa.Constrictor.Playwright
{
    /// <summary>
    /// Gets whether the element is visible
    /// </summary>
    public class Visibility : AbstractLocatorQuestion<bool>
    {
        #region Constructor

        private Visibility(IPlaywrightLocator locator, LocatorIsVisibleOptions options) : base(locator)
        {
            Options = options;
        }

        #endregion
        
        #region Properties

        private LocatorIsVisibleOptions Options { get; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target element.</param>
        /// <param name="options">Call options</param>
        /// <returns></returns>
        public static Visibility Of(IPlaywrightLocator locator, LocatorIsVisibleOptions options = null) => new Visibility(locator, options);

        #endregion

        #region Methods

        /// <summary>
        /// Gets whether the element is visible
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="locator">The target element.</param>
        /// <returns></returns>
        public override async Task<bool> RequestAsAsync(IActor actor, ILocator locator)
        {
            return await locator.IsVisibleAsync(Options);
        }

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"visibility of {Locator.Description}";

        #endregion
    }
}