using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Accepts a browser alert.
    /// </summary>
    public class AcceptAlert : AbstractWebTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="rethrow">If true, rethrow NoAlertPresentException if it occurs.</param>
        private AcceptAlert(bool rethrow) => RethrowNoAlert = rethrow;

        #endregion

        #region Properties

        /// <summary>
        /// If true, rethrow NoAlertPresentException if it occurs.
        /// </summary>
        private bool RethrowNoAlert { get; set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the task object.
        /// Will rethrow NoAlertPresentException.
        /// </summary>
        /// <returns></returns>
        public static AcceptAlert ThatMustExist() => new AcceptAlert(true);

        /// <summary>
        /// Constructs the task object.
        /// Will bury NoAlertPresentException quietly.
        /// </summary>
        /// <returns></returns>
        public static AcceptAlert IfItExists() => new AcceptAlert(false);

        #endregion

        #region Methods

        /// <summary>
        /// Navigates the browser to the target URL.
        /// </summary>
        /// <param name="actor">The screenplay actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().Alert().Accept();
            }
            catch (NoAlertPresentException)
            {
                if (RethrowNoAlert)
                    throw;
            }
        }

        /// <summary>
        /// Returns a description of the task.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string exists = RethrowNoAlert ? "that must exist" : "if it exists";
            return $"Accept browser alert {exists}";
        }

        #endregion
    }
}
