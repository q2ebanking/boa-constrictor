using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace Boa.Constrictor.Selenium
{
    /// <summary>
    /// A web locator that finds descendants inside an open shadow root.
    /// </summary>
    public class ShadowLocator : IWebLocator
    {
        #region Builder Methods

        /// <summary>
        /// Convenient builder method for constructing ShadowLocator objects.
        /// </summary>
        /// <param name="description">Plain-language description of the Web element (used for logging).</param>
        /// <param name="shadowHostBy">Locator for the shadow host web element. Must use a CSS selector.</param>
        /// <param name="shadowDescendantBy">Locator for the shadow descendant web element.</param>
        /// <returns>A new ShadowLocator instance.</returns>
        public static ShadowLocator S(string description, By shadowHostBy, By shadowDescendantBy) =>
            new ShadowLocator(description, shadowHostBy, shadowDescendantBy);

        /// <summary>
        /// Convenient builder method for constructing ShadowLocator objects.
        /// </summary>
        /// <param name="description">Plain-language description of the Web element (used for logging).</param>
        /// <param name="shadowHostBy">Locator for the shadow host web element. Must use a CSS selector.</param>
        /// <param name="shadowDescendantBy">Locator for the shadow descendant web element.</param>
        /// <returns>A new ShadowLocator instance.</returns>
        public static ShadowLocator Shadow(string description, By shadowHostBy, By shadowDescendantBy) =>
            new ShadowLocator(description, shadowHostBy, shadowDescendantBy);

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="description">Plain-language description of the Web element (used for logging).</param>
        /// <param name="shadowHostBy">Locator for the shadow host web element. Must use a CSS selector.</param>
        /// <param name="shadowDescendantBy">Locator for the shadow descendant web element.</param>
        public ShadowLocator(string description, By shadowHostBy, By shadowDescendantBy)
        {
            Description = description;

            if (!IsCss(shadowHostBy))
                throw new ArgumentException("Shadow host locator must be a CSS selector.", nameof(shadowHostBy));

            ShadowHostBy = shadowHostBy;
            ShadowDescendantBy = shadowDescendantBy;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Plain-language description of the Web element (used for logging).
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Query for the shadow host web element.
        /// </summary>
        public By ShadowHostBy { get; private set; }

        /// <summary>
        /// Query for the shadow descendant web element.
        /// </summary>
        public By ShadowDescendantBy { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if this locator is equal to another locator.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns></returns>
        public override bool Equals(object obj) =>
            obj is ShadowLocator locator &&
            Description == locator.Description &&
            EqualityComparer<By>.Default.Equals(ShadowHostBy, locator.ShadowHostBy) &&
            EqualityComparer<By>.Default.Equals(ShadowDescendantBy, locator.ShadowDescendantBy);

        /// <summary>
        /// Locates the web element.
        /// </summary>
        /// <param name="driver">The WebDriver.</param>
        public IWebElement FindElement(IWebDriver driver) =>
            GetShadowRoot(driver).FindElement(ShadowDescendantBy);

        /// <summary>
        /// Locates the web elements.
        /// </summary>
        /// <param name="driver">The WebDriver.</param>
        public ReadOnlyCollection<IWebElement> FindElements(IWebDriver driver) =>
            GetShadowRoot(driver).FindElements(ShadowDescendantBy);

        /// <summary>
        /// Gets a unique hash code for the locator.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() =>
            HashCode.Combine(GetType(), Description, ShadowHostBy, ShadowDescendantBy);

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"'{Description}' ({ShadowHostBy} {ShadowDescendantBy})";

        private ISearchContext GetShadowRoot(IWebDriver driver)
        {
            if (!(driver is IJavaScriptExecutor executor))
                throw new InvalidOperationException("The WebDriver must implement IJavaScriptExecutor to access shadow roots.");

            IWebElement host = driver.FindElement(ShadowHostBy);
            object shadowRoot = executor.ExecuteScript("return arguments[0].shadowRoot", host);

            if (!(shadowRoot is ISearchContext searchContext))
                throw new NoSuchElementException($"Shadow host '{Description}' does not expose an open shadow root.");

            return searchContext;
        }

        private static bool IsCss(By by)
        {
            if (by is null)
                return false;

            string query = by.ToString();
            return query.StartsWith("By.CssSelector:", StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}
