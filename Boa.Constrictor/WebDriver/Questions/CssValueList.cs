using OpenQA.Selenium;
using System;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets web elements' CSS values by property name.
    /// </summary>
    public class CssValueList : AbstractWebPropertyListQuestion
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="propertyName">The attribute name.</param>
        protected CssValueList(IWebLocator locator, string propertyName) : base(locator, propertyName) { }

        #endregion

        #region Properties

        /// <summary>
        /// Retrieves the Web element's CSS value by name.
        /// </summary>
        protected override Func<IWebElement, string> Retrieval => e => e.GetCssValue(PropertyName);

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="named">The attribute name.</param>
        /// <returns></returns>
        public static CssValueList For(IWebLocator locator, string named) => new CssValueList(locator, named);

        #endregion
    }
}
