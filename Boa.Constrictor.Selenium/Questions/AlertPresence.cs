using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Returns the presence of an alert in the browser.
    /// </summary>
    public class AlertPresence : AbstractWebQuestion<bool>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        private AlertPresence() { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <returns></returns>
        public static AlertPresence InBrowser() => new AlertPresence();

        #endregion

        #region Methods

        /// <summary>
        /// Gets the presence of an alert in the browser.
        /// Returns true if an alert is there; false otherwise.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override bool RequestAs(IActor actor, IWebDriver driver)
        {
            bool presence = false;

            try
            {
                driver.SwitchTo().Alert();
                presence = true;
            }
            catch (NoAlertPresentException) { }

            return presence;
        }

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => "browser alert presence";

        #endregion
    }
}
