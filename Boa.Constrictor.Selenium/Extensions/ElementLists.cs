using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Useful methods for handling multiple elements.
    /// </summary>
    public static class ElementLists
    {
        /// <summary>
        /// Finds all Web elements on the page matching the provided locator and gets a value from each.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="locator">Locator used to find Web elements.</param>
        /// <param name="getValue">The method used to obtain the desired value from each Web element.</param>
        /// <returns>A list of string values from each Web element found.</returns>
        public static IEnumerable<string> GetValues(IActor actor, IWebDriver driver, IWebLocator locator, Func<IWebElement, string> getValue)
        {
            actor.WaitsUntil(Existence.Of(locator), IsEqualTo.True());
            var elements = driver.FindElements(locator.Query);
            var strings = from e in elements select getValue(e);

            // ToList() will avoid lazy evaluation
            return strings.ToList();
        }
    }
}
