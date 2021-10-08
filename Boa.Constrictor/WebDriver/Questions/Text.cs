using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets a Web element's text.
    /// </summary>
    public class Text : AbstractWebLocatorQuestion<string>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private Text(IWebLocator locator) : base(locator) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static Text Of(IWebLocator locator) => new Text(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Gets the Web element's text.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override string RequestAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Existence.Of(Locator), IsEqualTo.True());
            return driver.FindElement(Locator.Query).Text;
        }

        #endregion
    }
}
