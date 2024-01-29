using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.Selenium
{
    /// <summary>
    /// Switches the frame.
    /// Start from DefaultContent by default.
    /// </summary>
    public class SwitchFrame : AbstractWebTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="locators">The list of locators.</param>
        /// <param name="useDefaultContent">If true use DefaultContent instead of the locators.</param>
        private SwitchFrame(List<IWebLocator> locators, bool useDefaultContent)
        {
            Locators = locators;
            StartFromCurrentLocation = false;
            UseDefaultContent = useDefaultContent;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The list of target Web element locators.
        /// </summary>
        public List<IWebLocator> Locators { get; }

        /// <summary>
        /// Start from current location instead of switching to DefaultContent first.
        /// </summary>
        public bool StartFromCurrentLocation { get; set; }

        /// <summary>
        /// Switch to DefaultContent instead of a target frame.
        /// </summary>
        public bool UseDefaultContent { get; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Task object for the given locator.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <returns></returns>
        public static SwitchFrame To(IWebLocator locator) =>
            new SwitchFrame(new List<IWebLocator> { locator }, false);

        /// <summary>
        /// Constructs the Task object for DefaultContent.
        /// </summary>
        /// <returns></returns>
        public static SwitchFrame ToDefaultContent() => new SwitchFrame(new List<IWebLocator>(), true);

        /// <summary>
        /// Constructs the Task object for the given locator list.
        /// </summary>
        /// <param name="locators">The list of locators.</param>
        /// <returns></returns>
        public static SwitchFrame ToNested(List<IWebLocator> locators) => new SwitchFrame(locators, false);

        /// <summary>
        /// Sets the Task to start from current location instead of DefaultContent.
        /// </summary>
        /// <returns></returns>
        public SwitchFrame AndStartFromCurrentLocation()
        {
            StartFromCurrentLocation = true;
            return this;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Switches to the frame given by the locator.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            if (UseDefaultContent || !StartFromCurrentLocation)
                driver.SwitchTo().DefaultContent();

            if (!UseDefaultContent)
            {
                foreach (IWebLocator locator in Locators)
                {
                    actor.WaitsUntil(Existence.Of(locator), IsEqualTo.True());
                    driver.SwitchTo().Frame(locator.FindElement(driver));
                }
            }
        }

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        public override bool Equals(object obj) =>
            obj is SwitchFrame frame &&
            Locators.Equals(frame.Locators) &&
            StartFromCurrentLocation == frame.StartFromCurrentLocation &&
            UseDefaultContent == frame.UseDefaultContent;

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() =>
            HashCode.Combine(GetType(), Locators, StartFromCurrentLocation, UseDefaultContent);

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            UseDefaultContent
                ? "switch frame to DefaultContent"
                : $"switch frame to '{Locators.Last().Description}'";

        #endregion
    }
}