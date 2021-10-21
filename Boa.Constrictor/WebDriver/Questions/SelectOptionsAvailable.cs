using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets the text list of a select Web element's options.
    /// </summary>
    public class SelectOptionsAvailable : AbstractWebLocatorQuestion<IList<string>>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private SelectOptionsAvailable(IWebLocator locator) : base(locator) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static SelectOptionsAvailable For(IWebLocator locator) => new SelectOptionsAvailable(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Gets the text list of a select Web element's options.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override IList<string> RequestAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Existence.Of(Locator), IsEqualTo.True());
            return new SelectElement(driver.FindElement(Locator.Query)).Options.Select(o => o.Text).ToList();
        }

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"text list of select options for '{Locator.Description}'";

        #endregion
    }
}
