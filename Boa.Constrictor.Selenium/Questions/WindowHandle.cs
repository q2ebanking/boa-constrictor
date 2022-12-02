using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets one of the WebDriver window handles.
    /// Getting the latest handle requires the SetTimeouts Ability.
    /// </summary>
    public class WindowHandle : AbstractWebQuestion<string>
    {
        #region Constants

        /// <summary>
        /// A constant representing the index for the first handle.
        /// </summary>
        public const int Index_First = 0;

        /// <summary>
        /// A constant representing the index for the last handle.
        /// </summary>
        public const int Index_Last = -1;

        /// <summary>
        /// A constant representing the index for the current handle.
        /// </summary>
        public const int Index_Current = -2;

        /// <summary>
        /// A constant representing the index for the latest handle.
        /// </summary>
        public const int Index_Latest = -3;

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="index">The index of the desired window handle.</param>
        private WindowHandle(int index) => Index = index;

        #endregion

        #region Properties

        /// <summary>
        /// The index of the desired window handle.
        /// </summary>
        private int Index { get; set; }
        
        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question to get the first handle.
        /// </summary>
        /// <returns></returns>
        public static WindowHandle First() => new WindowHandle(Index_First);

        /// <summary>
        /// Constructs the Question to get the last handle.
        /// </summary>
        /// <returns></returns>
        public static WindowHandle Last() => new WindowHandle(Index_Last);

        /// <summary>
        /// Constructs the Question to get the current handle.
        /// </summary>
        /// <returns></returns>
        public static WindowHandle Current() => new WindowHandle(Index_Current);

        /// <summary>
        /// Constructs the Question to get the latest handle.
        /// If more than one handle are new, navigates to the first one in the list.
        /// </summary>
        /// <returns></returns>
        public static WindowHandle Latest() => new WindowHandle(Index_Latest);

        /// <summary>
        /// Constructs the Question to get the handle at the given index.
        /// </summary>
        /// <param name="index">The index of the desired window handle.</param>
        /// <returns></returns>
        public static WindowHandle At(int index) => new WindowHandle(index);

        #endregion

        #region Private Methods

        /// <summary>
        /// Attempts to get the latest window handle.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        private string GetLatestHandle(IActor actor)
        {
            // Get Abilities
            BrowseTheWeb browseAbility = actor.Using<BrowseTheWeb>();
            SetTimeouts timeoutAbility = actor.Using<SetTimeouts>();

            // Create handle collection variables.
            IEnumerable<string> allHandles = null;
            IEnumerable<string> newHandles = null;

            // Start the timer
            int timeout = timeoutAbility.CalculateTimeout();
            Stopwatch timer = new Stopwatch();
            timer.Start();

            // Repeatedly check if the condition is satisfied until the timeout is reached
            do
            {
                allHandles = from h in browseAbility.WebDriver.WindowHandles select h;
                newHandles = allHandles.Except(browseAbility.StoredWindowHandles);
            }
            while (!newHandles.Any() && timer.Elapsed.TotalSeconds < timeout);

            // Stop the timer
            timer.Stop();

            // Verify successful waiting
            if (!newHandles.Any())
                throw new BrowserInteractionException("No new browser window appeared");

            // Store the collection of all handles as the new collection of "stored" handles.
            // That way, the WaitForNewHandles will find newer handles the next time it is called.
            // Use the cached handle set to avoid race conditions.
            // (If a fresh set of handles were retrieved here, then it might have new handles not in "newHandles".)
            browseAbility.StoredWindowHandles = allHandles;

            // Return the new handle
            return newHandles.First();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the desired WebDriver window handles.
        /// Throws a BrowserInteractionException if the index is out of bounds.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override string RequestAs(IActor actor, IWebDriver driver)
        {
            string handle;

            if (Index < Index_Latest || Index >= driver.WindowHandles.Count)
                throw new BrowserInteractionException($"No browser window exists at index '{Index}'");
            else if (Index == Index_Latest)
                handle = GetLatestHandle(actor);
            else if (Index == Index_Current)
                handle = driver.CurrentWindowHandle;
            else if (Index == Index_Last)
                handle = driver.WindowHandles.Last();
            else
                handle = driver.WindowHandles[Index];

            return handle;
        }

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <returns></returns>
        public override bool Equals(object obj) => obj is WindowHandle handle && Index == handle.Index;

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => HashCode.Combine(GetType(), Index);

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"window handle at index '{Index}'";

        #endregion
    }
}
