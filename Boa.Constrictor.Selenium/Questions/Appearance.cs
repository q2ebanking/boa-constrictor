using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets a Web element's displayed state.
    /// Note that an element can exist without being displayed,
    /// But it cannot be displayed without existing.
    /// Warning: This method does NOT wait for the element to exist first!
    /// </summary>
    public class Appearance : AbstractWebLocatorQuestion<bool>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private Appearance(IWebLocator locator) : base(locator) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static Appearance Of(IWebLocator locator) => new Appearance(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Returns true if the element exists and is displayed on the page; false otherwise.
        /// Note that an element can exist without being displayed,
        /// But it cannot be displayed without existing.
        /// Warning: This method does NOT wait for the element to exist first!
        /// Furthermore, if StaleElementReferenceException happens, this returns false.
        /// https://docs.seleniumhq.org/exceptions/stale_element_reference.jsp
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override bool RequestAs(IActor actor, IWebDriver driver)
        {
            bool appearance;

            try
            {
                appearance = driver.FindElement(Locator.Query).Displayed;
            }
            catch (NoSuchElementException)
            {
                appearance = false;
                // Don't log here. If the element isn't found, then it doesn't exist.
            }
            catch (StaleElementReferenceException e)
            {
                appearance = false;
                actor.Logger.Warning(e.ToString());
            }

            return appearance;
        }

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"appearance of '{Locator.Description}'";

        #endregion
    }
}
