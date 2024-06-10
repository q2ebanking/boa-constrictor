using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.Selenium
{
    /// <summary>
    /// Closes the WebDriver window or tab.
    /// </summary>
    public class CloseWebDriver : AbstractWebTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        private CloseWebDriver() { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Task object.
        /// </summary>
        /// <returns></returns>
        public static CloseWebDriver ForBrowser() => new CloseWebDriver();

        #endregion

        #region Methods

        /// <summary>
        /// Closes the WebDriver window or tab.
        /// WARNING: You must switch back to a valid window handle in order to continue execution
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver) => driver.Close();

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => "close the WebDriver window or tab";

        #endregion
    }
}
