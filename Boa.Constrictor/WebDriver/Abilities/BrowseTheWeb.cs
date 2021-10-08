using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Enables the Actor to use a Web browser via Selenium WebDriver.
    /// </summary>
    public class BrowseTheWeb : IAbility
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use the static methods for public construction.)
        /// </summary>
        /// <param name="driver">The WebDriver instance.</param>
        private BrowseTheWeb(IWebDriver driver)
        {
            WebDriver = driver;
            StoredWindowHandles = from h in WebDriver.WindowHandles select h;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The WebDriver instance.
        /// </summary>
        public IWebDriver WebDriver { get; internal set; }

        /// <summary>
        /// The collection of stored browser window handles.
        /// This collection should be referenced when searching for new handles.
        /// </summary>
        public IEnumerable<string> StoredWindowHandles { get; set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs this object with the given WebDriver.
        /// </summary>
        /// <param name="driver">The WebDriver instance.</param>
        /// <returns></returns>
        public static BrowseTheWeb With(IWebDriver driver) => new BrowseTheWeb(driver);

        #endregion

        #region Methods

        /// <summary>
        /// Returns a description of this Ability.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"browse the Web with {WebDriver}";

        #endregion
    }
}
