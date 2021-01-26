using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets web elements' HTML attributes by name.
    /// </summary>
    public class HtmlAttributeList : AbstractWebPropertyQuestion<IEnumerable<string>>
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

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="named">The attribute name.</param>
        /// <returns></returns>
        public static HtmlAttributeList For(IWebLocator locator, string named) => new HtmlAttributeList(locator, named);

        #endregion

        #region Methods

        /// <summary>
        /// Gets web elements' HTML attributes by name.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override IEnumerable<string> RequestAs(IActor actor, IWebDriver driver) =>
            ElementLists.GetValues(actor, driver, Locator, e => e.GetAttribute(PropertyName));

        #endregion
    }
}
