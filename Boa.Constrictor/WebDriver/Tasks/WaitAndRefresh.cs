using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Waits for an element to appear and refreshes the browser if it doesn't appear within the refresh timeout.
    /// Internally calls Wait.
    /// </summary>
    public class WaitAndRefresh : AbstractWebLocatorTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        private WaitAndRefresh(IWebLocator locator):
            base(locator)
        {
            RefreshSeconds = 3;
            TimeoutSeconds = null;
            AdditionalSeconds = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The number of seconds to sleep after refreshing the browser.
        /// </summary>
        public int RefreshSeconds { get; private set; }

        /// <summary>
        /// The timeout override in seconds.
        /// If null, use the standard timeout value.
        /// This value is passed directly into Wait.
        /// </summary>
        public int? TimeoutSeconds { get; private set; }

        /// <summary>
        /// An additional amount to add to the timeout.
        /// </summary>
        public int AdditionalSeconds { get; protected set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Task.
        /// </summary>
        /// <param name="locator">The locator for whose appearance to wait.</param>
        /// <returns></returns>
        public static WaitAndRefresh For(IWebLocator locator) => new WaitAndRefresh(locator);

        /// <summary>
        /// Sets an override value for timeout seconds.
        /// This value is passed directly into Wait.
        /// </summary>
        /// <param name="seconds">The new timeout in seconds.</param>
        /// <returns></returns>
        public WaitAndRefresh ForUpTo(int seconds)
        {
            TimeoutSeconds = seconds;
            return this;
        }

        /// <summary>
        /// Adds an additional amount to the timeout.
        /// </summary>
        /// <param name="seconds">The seconds to add to the timeout.</param>
        /// <returns></returns>
        public WaitAndRefresh ForAnAdditional(int seconds)
        {
            AdditionalSeconds = seconds;
            return this;
        }

        /// <summary>
        /// Sets an override value for time to wait after refreshing the browser.
        /// </summary>
        /// <param name="seconds">Wait time in seconds.</param>
        /// <returns></returns>
        public WaitAndRefresh RefreshWaiting(int seconds)
        {
            RefreshSeconds = seconds;
            return this;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Waits for an element to appear and refreshes the browser if it doesn't appear within the refresh timeout.
        /// Internally calls Wait.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            // Construct the waiting interaction
            var wait = Wait.Until(Appearance.Of(Locator), IsEqualTo.True())
                .ForUpTo(TimeoutSeconds).ForAnAdditional(AdditionalSeconds);

            try
            {
                // Wait for the button to appear
                // This page notoriously takes longer to load than others
                actor.AttemptsTo(wait);
            }
            catch (WaitingException<bool>)
            {
                // If the button doesn't load, refresh the browser and retry
                // That's what a human would do
                actor.AttemptsTo(Refresh.Browser());
                System.Threading.Thread.Sleep(RefreshSeconds * 1000);
                actor.AttemptsTo(wait);
            }
        }

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        public override bool Equals(object obj) =>
            obj is WaitAndRefresh refresh &&
            EqualityComparer<IWebLocator>.Default.Equals(Locator, refresh.Locator) &&
            RefreshSeconds == refresh.RefreshSeconds &&
            TimeoutSeconds == refresh.TimeoutSeconds &&
            AdditionalSeconds == refresh.AdditionalSeconds;

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() =>
            HashCode.Combine(GetType(), Locator, RefreshSeconds, TimeoutSeconds, AdditionalSeconds);

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string message = $"wait for '{Locator.Description}' to appear";

            if (TimeoutSeconds != null)
                message += $" for up to {TimeoutSeconds + AdditionalSeconds}s";

            message += " with a refresh if necessary";

            return message;
        }

        #endregion
    }
}
