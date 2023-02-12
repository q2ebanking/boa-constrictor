using System;
using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.Selenium
{
    /// <summary>
    /// Gets the ShadowRoot for the locator.
    /// </summary>
    public class ShadowRoot : AbstractWebLocatorQuestion<ISearchContext>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private ShadowRoot(IWebLocator locator) : base(locator) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static ShadowRoot ForShadowHost(IWebLocator locator) =>
            new ShadowRoot(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Gets the ShadowRoot for the locator.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override ISearchContext RequestAs(IActor actor, IWebDriver driver)
        {
            try
            {
                ISearchContext shadowRoot = Locator.FindElement(driver).GetShadowRoot();
                return shadowRoot;
            }
            catch (Exception e)
            {
                actor.Logger.Warning($"{this} not found");
                actor.Logger.Warning(e.Message);
                // throw;
                return null;
            }
        }

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"ShadowRoot for '{Locator.Description}'";

        #endregion
    }
}