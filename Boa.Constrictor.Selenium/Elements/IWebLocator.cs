﻿using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace Boa.Constrictor.Selenium
{
    /// <summary>
    /// An interface for locating Web elements on a page.
    /// </summary>
    public interface IWebLocator
    {
        #region Properties

        /// <summary>
        /// Plain-language description of the Web element (used for logging).
        /// </summary>
        string Description { get; }

        #endregion

        #region Methods

        //TODO: Remove once no longer needed
        /// <summary>
        /// Locates the Web element.
        /// </summary>
        /// <param name="driver">The WebDriver.</param>
        IWebElement FindElement(IWebDriver driver);

        /// <summary>
        /// Locates the Web elements.
        /// </summary>
        /// <param name="driver">The WebDriver.</param>
        ReadOnlyCollection<IWebElement> FindElements(IWebDriver driver);

        #endregion
    }
}