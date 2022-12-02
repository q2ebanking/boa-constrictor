using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets a Web element's existence.
    /// "Existence" means that the element is present in the DOM.
    /// Note that an element can exist without being displayed.
    /// Warning: This method does NOT wait for the element to exist first!
    /// </summary>
    public class Existence : AbstractWebLocatorQuestion<bool>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private Existence(IWebLocator locator) : base(locator) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static Existence Of(IWebLocator locator) => new Existence(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Returns true if at least one element exists on the page for the locator; false otherwise.
        /// "Existence" means that the element is present in the DOM.
        /// Note that an element can exist without being displayed.
        /// Warning: This method does NOT wait for the element to exist first!
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override bool RequestAs(IActor actor, IWebDriver driver) =>
            driver.FindElements(Locator.Query).Count > 0;

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"existence of '{Locator.Description}'";

        #endregion
    }
}
