using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets the total number of elements found on the page by the locator.
    /// </summary>
    public class Count : AbstractWebLocatorQuestion<int>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private Count(IWebLocator locator) : base(locator) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static Count Of(IWebLocator locator) =>
            new Count(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Gets the total number of elements found on the page by the locator.
        /// Don't wait because count should be immediate.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override int RequestAs(IActor actor, IWebDriver driver) =>
            driver.FindElements(Locator.Query).Count;

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"count of elements found by '{Locator.Description}'";

        #endregion
    }
}
