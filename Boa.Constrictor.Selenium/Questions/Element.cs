using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.Selenium
{
    internal class Element : AbstractWebLocatorQuestion<IWebElement>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private Element(IWebLocator locator) : base(locator) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static Element At(IWebLocator locator) => new Element(locator);

        #endregion

        #region Methods

        public override IWebElement RequestAs(IActor actor, IWebDriver driver)
        {
            IWebElement element = null;

            if (Locator is WebLocator webLocator)
            {
                // TODO: Appearance vs. Existence
                actor.WaitsUntil(Appearance.Of(webLocator), IsEqualTo.True());
                element = driver.FindElement(webLocator.Query);
            }
            else if (Locator is ShadowLocator shadowLocator)
            {
                actor.WaitsUntil(Existence.Of(shadowLocator.Host), IsEqualTo.True());
                ISearchContext shadowRoot = driver.FindElement(shadowLocator.Host.Query).GetShadowRoot();

                // TODO: Add waiting
                element = shadowRoot.FindElement(shadowLocator.Target.Query);
            }

            return element;
        }

        #endregion
    }
}
