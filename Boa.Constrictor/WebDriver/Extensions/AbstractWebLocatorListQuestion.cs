using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Abstract class for any Web questions that return a collection of strings using a Web locator and a property name.
    /// </summary>
    public abstract class AbstractWebLocatorListQuestion : AbstractWebLocatorQuestion<IEnumerable<string>>
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        public AbstractWebLocatorListQuestion(IWebLocator locator) : base(locator) { }

        #endregion

        #region Properties

        /// <summary>
        /// Method that returns a string from a Web element input.
        /// </summary>
        protected abstract Func<IWebElement, string> Retrieval { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the requested value from each element.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override IEnumerable<string> RequestAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Existence.Of(Locator), IsEqualTo.True());
            var elements = driver.FindElements(Locator.Query);
            var strings = from e in elements select Retrieval(e);

            // ToList() will avoid lazy evaluation
            return strings.ToList();
        }

        #endregion
    }
}
