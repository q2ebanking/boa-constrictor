using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets a Web element's selected state.
    /// </summary>
    public class SelectedState : AbstractWebLocatorQuestion<bool>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private SelectedState(IWebLocator locator) : base(locator) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static SelectedState Of(IWebLocator locator) => new SelectedState(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Returns true if the element is selected; otherwise false.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override bool RequestAs(IActor actor, IWebDriver driver)
        {
            actor.AttemptsTo(Wait.Until(Existence.Of(Locator), IsEqualTo.True()));
            return driver.FindElement(Locator.Query).Selected;
        }

        #endregion
    }
}
