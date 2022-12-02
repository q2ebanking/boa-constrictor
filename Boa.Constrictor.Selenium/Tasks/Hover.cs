using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Hovers over a Web element.
    /// </summary>
    public class Hover : AbstractWebLocatorTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private Hover(IWebLocator locator) : base(locator) { }

        #endregion
        
        #region Builder Methods

        /// <summary>
        /// Constructs the Task object.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static Hover Over(IWebLocator locator) => new Hover(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Hovers over the web element.
        /// Make sure the proper locator is used, or else hovering may have no effect!
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Appearance.Of(Locator), IsEqualTo.True());
            new Actions(driver).MoveToElement(driver.FindElement(Locator.Query)).Perform();
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"hover over '{Locator.Description}'";

        #endregion
    }
}
