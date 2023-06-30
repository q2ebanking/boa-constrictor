using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;

namespace Boa.Constrictor.Selenium
{
    public class ShadowLocator : IWebLocator
    {
        #region Properties

        /// <summary>
        /// Plain-language description of the Web element (used for logging).
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Query for the shadow host Web element.
        /// </summary>
        public WebLocator Host { get; private set; }

        /// <summary>
        /// Query for the Web element within the shadow host.
        /// </summary>
        public WebLocator Target { get; private set; }

        #endregion

        #region Constructors

        // TODO: update descriptions
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="description">Plain-language description of the Web element (used for logging).</param>
        /// <param name="host">Locator for the shadow host Web element.</param>
        /// <param name="target">Locator for the Web element within the shadow host.</param>
        public ShadowLocator(string description, WebLocator host, WebLocator target)
        {
            Description = description;
            Host = host;
            Target = target;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="description">Plain-language description of the Web element (used for logging).</param>
        /// <param name="host">Query for the shadow host Web element.</param>
        /// <param name="target">Query for the Web element within the shadow host.</param>
        public ShadowLocator(string description, By host, By target)
        {
            Description = description;
            Host = new WebLocator("Shadow Host", host);
            Target = new WebLocator("Target Element", target);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if this shadow locator is equal to another shadow locator.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns></returns>
        public override bool Equals(object obj) =>
            obj is ShadowLocator locator &&
            Description == locator.Description &&
            Host.Equals(locator.Host) &&
            Target.Equals(locator.Target);

        // Stub to implement the interface
        public IWebElement FindElement(IWebDriver driver) => null;

        // Stub to implement the interface
        public ReadOnlyCollection<IWebElement> FindElements(IWebDriver driver) => null;

        /// <summary>
        /// Gets a unique hash code for the shadow locator.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() =>
            HashCode.Combine(GetType(), Description, Host, Target);

        // TODO: Come back to this
        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"'{Description}' (shadow host: {Host}, target element: {Target})";

        #endregion
    }
}
