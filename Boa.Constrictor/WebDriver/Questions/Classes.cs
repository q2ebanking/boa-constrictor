﻿using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets the list of the Web element's CSS classes.
    /// </summary>
    public class Classes : AbstractWebLocatorQuestion<string[]>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private Classes(IWebLocator locator) : base(locator) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static Classes Of(IWebLocator locator) => new Classes(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Gets the list of the Web element's CSS classes.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override string[] RequestAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Existence.Of(Locator), IsEqualTo.True());
            string classes = driver.FindElement(Locator.Query).GetAttribute("class");
            return classes.Split();
        }

        #endregion
    }
}
