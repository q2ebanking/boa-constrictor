using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets a web element's HTML attribute by name.
    /// </summary>
    public class HtmlAttribute : AbstractWebPropertyQuestion<string>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="propertyName">The attribute name.</param>
        protected HtmlAttribute(IWebLocator locator, string propertyName) : base(locator, propertyName) { }

        #endregion
        
        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="named">The attribute name.</param>
        /// <returns></returns>
        public static HtmlAttribute Of(IWebLocator locator, string named) => new HtmlAttribute(locator, named);

        #endregion

        #region Methods

        /// <summary>
        /// Gets a web element's HTML attribute by name.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override string RequestAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Existence.Of(Locator), IsEqualTo.True());
            return driver.FindElement(Locator.Query).GetAttribute(PropertyName);
        }

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"HTML attribute of '{PropertyName}' for '{Locator.Description}'";

        #endregion
    }

    /// <summary>
    /// Gets a web element's "id" attribute.
    /// </summary>
    public static class IdAttribute
    {
        #region Constants

        /// <summary>
        /// The "id" attribute name.
        /// </summary>
        public const string Id = "id";

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs a HtmlAttribute Question for the "id" attribute.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static HtmlAttribute Of(IWebLocator locator) => HtmlAttribute.Of(locator, Id);

        #endregion
    }

    /// <summary>
    /// Gets a web element's "value" attribute.
    /// </summary>
    public static class ValueAttribute
    {
        #region Constants

        /// <summary>
        /// The "value" attribute name.
        /// </summary>
        public const string Value = "value";

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs a HtmlAttribute Question for the "value" attribute.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static HtmlAttribute Of(IWebLocator locator) => HtmlAttribute.Of(locator, Value);

        #endregion
    }
}
