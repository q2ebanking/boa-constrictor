using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets a web element's CSS value by property name.
    /// </summary>
    public class CssValue : AbstractWebPropertyQuestion<string>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="propertyName">The attribute name.</param>
        private CssValue(IWebLocator locator, string propertyName) : base(locator, propertyName) { }

        #endregion
        
        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="named">The property name.</param>
        /// <returns></returns>
        public static CssValue Of(IWebLocator locator, string named) =>
            new CssValue(locator, named);

        #endregion

        #region Methods

        /// <summary>
        /// Gets a web element's CSS value by property name.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override string RequestAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Existence.Of(Locator), IsEqualTo.True());
            return driver.FindElement(Locator.Query).GetCssValue(PropertyName);
        }

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"CSS value of '{PropertyName}' for '{Locator.Description}'";

        #endregion
    }
}
