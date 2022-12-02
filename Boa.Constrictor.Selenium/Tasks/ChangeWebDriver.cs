using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Changes the WebDriver instance in the Actor's BrowseTheWeb Ability.
    /// Sometimes, WebDriver instances can arbitrarily fail due to problems with the tool, not the web app under test.
    /// This Task enables the Actor to "recover" by providing it with a new WebDriver instance.
    /// 
    /// USE THIS TASK WITH CAUTION!
    /// Changing the WebDriver instance could be dangerous.
    /// Make sure to quit the old WebDriver first.
    /// Also make sure that automation can continue after the WebDriver change.
    /// </summary>
    public class ChangeWebDriver : AbstractWebTask
    {
        #region Properties

        /// <summary>
        /// The new WebDriver instance.
        /// </summary>
        public IWebDriver NewDriver { get; }

        /// <summary>
        /// If true, attempt to quit the old WebDriver instance before replacing it.
        /// </summary>
        public bool QuitOldDriver { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use public builder methods instead.)
        /// </summary>
        /// <param name="newDriver">The new WebDriver instance.</param>
        /// <param name="quitOldDriver">If true, attempt to quit the old WebDriver instance before replacing it.</param>
        private ChangeWebDriver(IWebDriver newDriver, bool quitOldDriver = false)
        {
            NewDriver = newDriver;
            QuitOldDriver = quitOldDriver;
        }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Creates the Task.
        /// </summary>
        /// <returns></returns>
        public static ChangeWebDriver To(IWebDriver newDriver) => new ChangeWebDriver(newDriver);
        
        /// <summary>
        /// Forces this Task to quit the old WebDriver instance before changing to the new one.
        /// </summary>
        /// <returns></returns>
        public ChangeWebDriver AfterQuittingOldWebDriver()
        {
            QuitOldDriver = true;
            return this;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Refreshes the browser.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            if (QuitOldDriver)
            {
                actor.Logger.Info("Attempting to quit the current WebDriver instance");

                try
                {
                    actor.AttemptsTo(QuitWebDriver.ForBrowser());
                }
                catch (Exception e)
                {
                    actor.Logger.Warning("Failed to quit the current WebDriver instance");
                    actor.Logger.Warning(e.Message);
                }
            }

            actor.Logger.Info("Setting the new WebDriver instance in the Actor's BrowseTheWeb Ability");
            actor.Using<BrowseTheWeb>().WebDriver = NewDriver;
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"change the WebDriver instance to a new '{NewDriver.GetType()}'";

        #endregion
    }
}
