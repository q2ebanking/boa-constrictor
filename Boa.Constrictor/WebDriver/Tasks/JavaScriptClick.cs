using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Clicks an element directly using JavaScript.
    /// Warning: Do this ONLY if conventional clicking does not work.
    /// This should be a last-ditch effort because it does not exercise the page like a normal user.
    /// </summary>
    public class JavaScriptClick : AbstractWebLocatorTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private JavaScriptClick(IWebLocator locator) : base(locator) { }

        #endregion
        
        #region Builder Methods

        /// <summary>
        /// Constructs the Task object.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static JavaScriptClick On(IWebLocator locator) => new JavaScriptClick(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Clicks the web element.
        /// Use browser actions instead of direct click (due to IE).
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver) =>
            actor.Calls(JavaScript.On(Locator, "arguments[0].click();"));

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"use JavaScript to click on '{Locator.Description}'";

        #endregion
    }
}
