using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets a web element's HTML attribute by name.
    /// </summary>
    public class HtmlAttribute : AbstractWebPropertyQuestion
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
        /// Constructs the question.
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
        /// <param name="actor">The actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override string RequestAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Existence.Of(Locator), IsEqualTo.True());
            return driver.FindElement(Locator.Query).GetAttribute(PropertyName);
        }
        
        #endregion
    }
}
