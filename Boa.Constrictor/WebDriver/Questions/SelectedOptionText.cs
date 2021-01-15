﻿using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets the text of a select Web element's selected option.
    /// </summary>
    public class SelectedOptionText : AbstractWebLocatorQuestion<string>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private SelectedOptionText(IWebLocator locator) : base(locator) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static SelectedOptionText Of(IWebLocator locator) => new SelectedOptionText(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Gets the text of a select Web element's selected option.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override string RequestAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Existence.Of(Locator), IsEqualTo.True());
            return new SelectElement(driver.FindElement(Locator.Query)).SelectedOption.Text;
        }

        #endregion
    }
}
