using OpenQA.Selenium;
using System;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets web elements' HTML attributes by name.
    /// </summary>
    public class HtmlAttributeList : AbstractWebPropertyListQuestion
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="propertyName">The attribute name.</param>
        protected HtmlAttributeList(IWebLocator locator, string propertyName) : base(locator, propertyName) { }

        #endregion

        #region Properties

        /// <summary>
        /// Retrieves the Web element's HTML attribute by name.
        /// </summary>
        protected override Func<IWebElement, string> Retrieval => e => e.GetAttribute(PropertyName);

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="named">The attribute name.</param>
        /// <returns></returns>
        public static HtmlAttributeList For(IWebLocator locator, string named) => new HtmlAttributeList(locator, named);

        #endregion
    }
}
