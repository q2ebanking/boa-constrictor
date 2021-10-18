using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets a web element's JavaScript textContent value.
    /// As a best practice, use the Text interaction instead.
    /// Use JavaScriptText only when direct JavaScript values are needed.
    /// </summary>
    public class JavaScriptText : AbstractWebLocatorQuestion<string>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private JavaScriptText(IWebLocator locator) : base(locator) { }

        #endregion
        
        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static JavaScriptText Of(IWebLocator locator) => new JavaScriptText(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Gets a web element's JavaScript textContent value.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override string RequestAs(IActor actor, IWebDriver driver) =>
            actor.Calls(JavaScript<string>.On(Locator, "return arguments[0].textContent;")).ToString();

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"JavaScript text for '{Locator.Description}'";

        #endregion
    }
}
