using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Double clicks a Web element.
    /// </summary>
    public class DoubleClick : AbstractWebLocatorTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private DoubleClick(IWebLocator locator) : base(locator) { }

        #endregion
        
        #region Builder Methods

        /// <summary>
        /// Constructs the Task object.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static DoubleClick On(IWebLocator locator) => new DoubleClick(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Double Clicks the web element.
        /// Use browser actions instead of direct click (due to IE).
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Appearance.Of(Locator), IsEqualTo.True());
            new Actions(driver).MoveToElement(driver.FindElement(Locator.Query)).DoubleClick().Perform();
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"double-click on '{Locator.Description}'";

        #endregion
    }
}
