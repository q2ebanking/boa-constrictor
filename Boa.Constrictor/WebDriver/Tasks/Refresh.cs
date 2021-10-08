using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Refreshes the page in the browser.
    /// </summary>
    public class Refresh : AbstractWebTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use public builder methods instead.)
        /// </summary>
        private Refresh() { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Creates the Task.
        /// </summary>
        /// <returns></returns>
        public static Refresh Browser() => new Refresh();
        
        #endregion

        #region Methods

        /// <summary>
        /// Refreshes the browser.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver) =>
            driver.Navigate().Refresh();

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => "refresh the web page";

        #endregion
    }
}
