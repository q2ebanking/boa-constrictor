using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Sends keystrokes to a Web element.
    /// By default, the element is cleared before keys are sent using backspaces.
    /// Sometimes, the "Clear" method doesn't work.
    /// Ctrl-A + Backspace doesn't always work, either.
    /// </summary>
    public class SendKeys : AbstractWebLocatorTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="keystrokes">The keystrokes (e.g., text) to send.</param>
        private SendKeys(IWebLocator locator, string keystrokes) : base(locator)
        {
            Clear = true;
            FinalElement = null;
            FinalEnter = false;
            Keystrokes = keystrokes;
            Private = false;
            UseClearMethod = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// If true, clear the element first.
        /// </summary>
        private bool Clear { get; set; }

        /// <summary>
        /// The locator for the element to click after sending the keys.
        /// Clicking another element can "commit" an input element's text.
        /// </summary>
        private IWebLocator FinalElement { get; set; }

        /// <summary>
        /// If true, hit the ENTER key after sending the keys.
        /// </summary>
        private bool FinalEnter { get; set; }

        /// <summary>
        /// The keystrokes (e.g., text) to send.
        /// </summary>
        private string Keystrokes { get; set; }

        /// <summary>
        /// If true, don't log the keystrokes.
        /// </summary>
        private bool Private { get; set; }

        /// <summary>
        /// If true, use the "Clear" method instead of backspaces to clear the element.
        /// This will happen only if the "Clear" property is true.
        /// </summary>
        private bool UseClearMethod { get; set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Task object.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="keystrokes">The keystrokes (e.g., text) to send.</param>
        /// <returns></returns>
        public static SendKeys To(IWebLocator locator, string keystrokes) => 
            new SendKeys(locator, keystrokes);

        /// <summary>
        /// Prevents keystrokes from being logged.
        /// </summary>
        /// <returns></returns>
        public SendKeys Privately()
        {
            Private = true;
            return this;
        }

        /// <summary>
        /// After sending keys to the target element, click a final element.
        /// Clicking another element can "commit" an input element's text.
        /// This will happen after hitting the ENTER key on the target element (if applicable).
        /// </summary>
        /// <param name="finalElement"></param>
        /// <returns></returns>
        public SendKeys ThenClick(IWebLocator finalElement)
        {
            FinalElement = finalElement;
            return this;
        }

        /// <summary>
        /// After sending keys to the target element, hit the ENTER key.
        /// This will happen before clicking the final element (if applicable).
        /// </summary>
        /// <returns></returns>
        public SendKeys ThenHitEnter()
        {
            FinalEnter = true;
            return this;
        }

        /// <summary>
        /// Use the "Clear" method instead of backspaces to clear the element.
        /// </summary>
        /// <returns></returns>
        public SendKeys UsingClearMethod()
        {
            Clear = true;
            UseClearMethod = true;
            return this;
        }

        /// <summary>
        /// Clearing the element before entering keystrokes is the default behavior.
        /// Call this method if clearing should not be done.
        /// </summary>
        /// <returns></returns>
        public SendKeys WithoutClearing()
        {
            Clear = false;
            UseClearMethod = false;
            return this;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends keystrokes to the target Web element.
        /// By default, the element will be cleared first, and keystrokes will not be kept private for logging.
        /// Use builder methods to change those defaults.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            // Wait for the element to exist
            actor.WaitsUntil(Appearance.Of(Locator), IsEqualTo.True());

            // Get the element
            IWebElement element = driver.FindElement(Locator.Query);

            // Clear the element if appropriate
            if (Clear)
            {
                if (UseClearMethod)
                {
                    // Use the plain-old "Clear" method
                    element.Clear();
                }
                else
                {
                    // How many backspaces should be sent?
                    // One for each character in the input!
                    int length = element.GetAttribute("value").Length;

                    if (length > 0)
                    {
                        // Send the backspaces
                        string backspaces = string.Concat(Enumerable.Repeat(Keys.Backspace, length));
                        element.SendKeys(backspaces);

                        // The browser may put the cursor to the left instead of the right
                        // Do the same thing for delete button
                        string deletes = string.Concat(Enumerable.Repeat(Keys.Delete, length));
                        element.SendKeys(deletes);
                    }
                }
            }

            // Send the keys to the element
            element.SendKeys(Keystrokes);

            // Hit the ENTER key if applicable
            if (FinalEnter)
                element.SendKeys(Keys.Enter);

            // Click on the final "safe" element if given
            if (FinalElement != null)
                actor.AttemptsTo(Click.On(FinalElement));
        }

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        public override bool Equals(object obj) =>
            obj is SendKeys keys &&
            EqualityComparer<IWebLocator>.Default.Equals(Locator, keys.Locator) &&
            Clear == keys.Clear &&
            EqualityComparer<IWebLocator>.Default.Equals(FinalElement, keys.FinalElement) &&
            FinalEnter == keys.FinalEnter &&
            Keystrokes == keys.Keystrokes &&
            Private == keys.Private &&
            UseClearMethod == keys.UseClearMethod;

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() =>
            HashCode.Combine(GetType(), Locator, Clear, FinalElement, FinalEnter, Keystrokes, Private, UseClearMethod);

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string clearDesc = UseClearMethod ? "the 'Clear' method" : "backspaces";
            string actionDesc = Clear ? $"clear using {clearDesc}, then send" : "send";
            string keyDesc = Private ? "private keys" : $"keys '{Keystrokes}'";
            string fullDesc = $"{actionDesc} {keyDesc} to {Locator.Description}";

            if (FinalEnter)
                fullDesc += ", then hit ENTER";
            if (FinalElement != null)
                fullDesc += $", then click '{FinalElement.Description}'";

            return fullDesc;
        }

        #endregion
    }
}
