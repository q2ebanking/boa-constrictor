using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets web elements' CSS values by property name.
    /// </summary>
    public class CssValueList : AbstractWebPropertyQuestion<IEnumerable<string>>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="propertyName">The attribute name.</param>
        private CssValueList(IWebLocator locator, string propertyName) : base(locator, propertyName) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="named">The attribute name.</param>
        /// <returns></returns>
        public static CssValueList For(IWebLocator locator, string named) => new CssValueList(locator, named);

        #endregion

        #region Methods

        /// <summary>
        /// Gets web elements' CSS values by property name.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override IEnumerable<string> RequestAs(IActor actor, IWebDriver driver) =>
            ElementLists.GetValues(actor, driver, Locator, e => e.GetCssValue(PropertyName));

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"list of CSS values of '{PropertyName}' for '{Locator.Description}'";

        #endregion
    }
}
