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
        /// Constructs the Question.
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
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override IEnumerable<string> RequestAs(IActor actor, IWebDriver driver) =>
            ElementLists.GetValues(actor, driver, Locator, e => e.GetAttribute(PropertyName));

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"list of HTML attributes of '{PropertyName}' for '{Locator.Description}'";

        #endregion
    }

    /// <summary>
    /// Gets a list of Web elements' id values.
    /// Useful when working with a group of Web elements whose ids are unique, but not consistent.
    /// </summary>
    public static class IdAttributeList
    {
        #region Constants

        /// <summary>
        /// The "id" attribute name.
        /// </summary>
        public const string Id = "id";

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs a HtmlAttributeList Question for the "id" attribute.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static HtmlAttributeList For(IWebLocator locator) => HtmlAttributeList.For(locator, Id);

        #endregion
    }

    /// <summary>
    /// Gets a list of Web elements' "value" values.
    /// </summary>
    public static class ValueAttributeList
    {
        #region Constants

        /// <summary>
        /// The "value" attribute name.
        /// </summary>
        public const string Value = "value";

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs a HtmlAttributeList Question for the "value" attribute.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static HtmlAttributeList For(IWebLocator locator) => HtmlAttributeList.For(locator, Value);

        #endregion
    }
}
