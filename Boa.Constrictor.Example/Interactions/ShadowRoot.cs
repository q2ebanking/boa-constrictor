using System;
using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.Selenium
{
    /// <summary>
    /// Gets the ShadowRoot for the locator.
    /// </summary>
    //public class ShadowRoot : AbstractWebLocatorQuestion<ISearchContext>
    public class ShadowRoot : AbstractWebLocatorQuestion<string>
    {
        #region Constructors

    /// <summary>
    /// Private constructor.
    /// (Use static methods for public construction.)
    /// </summary>
    /// <param name="locator">The target Web element's locator.</param>
    private ShadowRoot(IWebLocator shadowRootLocator, WebLocator shadowContentLocator) : base(shadowContentLocator){
        
        ShadowRootLocator = shadowRootLocator;
        ShadowContentLocator = shadowContentLocator;
    } 

    #endregion

    #region Props

    public IWebLocator ShadowRootLocator;
    public WebLocator ShadowContentLocator;
    
    #endregion

    #region Builder Methods

    /// <summary>
    /// Constructs the Question.
    /// </summary>
    /// <param name="locator">The target Web element's locator.</param>
    /// <returns></returns>
    public static ShadowRoot TextForShadowContentElement(IWebLocator shadowRootLocator, WebLocator shadowContentLocator) =>
            new ShadowRoot(shadowRootLocator, shadowContentLocator);

        #endregion

        #region Methods

        /// <summary>
        /// Gets the ShadowRoot for the locator.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        //public override ISearchContext RequestAs(IActor actor, IWebDriver driver)
        public override string RequestAs(IActor actor, IWebDriver driver)
        {
            ISearchContext shadowRoot = ShadowRootLocator.FindElement(driver).GetShadowRoot();
            IWebElement shadowContentElement = shadowRoot.FindElement(ShadowContentLocator.Query);
            string shadowElementText = shadowContentElement.Text;

            //try
            //{
            //    ISearchContext shadowRoot = Locator.FindElement(driver).GetShadowRoot();


            //    //return shadowRoot;
            //}
            //catch (Exception e)
            //{
            //    actor.Logger.Warning($"{this} not found");
            //    actor.Logger.Warning(e.Message);
            //    // throw;
            //    return null;
            //}
            return shadowElementText;
        }

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"ShadowRoot for '{Locator.Description}'";

        #endregion
    }
}