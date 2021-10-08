using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Quits the WebDriver.
    /// WARNING: Once a WebDriver is quit, it cannot be used again.
    /// </summary>
    public class QuitWebDriver : AbstractWebTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        private QuitWebDriver() { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Task object.
        /// </summary>
        /// <returns></returns>
        public static QuitWebDriver ForBrowser() => new QuitWebDriver();

        #endregion

        #region Methods

        /// <summary>
        /// Quits the WebDriver.
        /// WARNING: Do NOT call this from steps!
        /// Once a WebDriver is quit, it cannot be used again.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver) => driver.Quit();

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => "quit the WebDriver";

        #endregion
    }
}
