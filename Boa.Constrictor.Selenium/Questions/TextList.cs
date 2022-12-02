using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets a list of Web elements' text values.
    /// </summary>
    public class TextList : AbstractWebLocatorQuestion<IEnumerable<string>>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private TextList(IWebLocator locator) : base(locator) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static TextList For(IWebLocator locator) => new TextList(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Gets the Web element's text.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override IEnumerable<string> RequestAs(IActor actor, IWebDriver driver) =>
            ElementLists.GetValues(actor, driver, Locator, e => e.Text);

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"text list of '{Locator.Description}'";

        #endregion
    }
}
