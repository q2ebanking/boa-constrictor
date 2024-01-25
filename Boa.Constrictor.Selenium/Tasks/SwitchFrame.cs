using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System;

namespace Boa.Constrictor.Selenium
{
    /// <summary>
    /// Switches the frame.
    /// </summary>
    public class SwitchFrame : AbstractWebTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="startFromDefaultContent">If true switch to DefaultContent before switching to the locator.</param>
        /// <param name="useDefaultContent">If true use DefaultContent instead of the locator.</param>
        private SwitchFrame(IWebLocator locator, bool useDefaultContent, bool startFromDefaultContent = true)
        {
            Locator = locator;
            StartFromDefaultContent = startFromDefaultContent;
            UseDefaultContent = useDefaultContent;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The target Web element's locator.
        /// </summary>
        public IWebLocator Locator { get; }

        /// <summary>
        /// Switch to DefaultContent first before switching to a frame.
        /// </summary>
        public bool StartFromDefaultContent { get; }

        /// <summary>
        /// The DefaultContent is used.
        /// </summary>
        public bool UseDefaultContent { get; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Task object for the given locator.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <returns></returns>
        public static SwitchFrame To(IWebLocator locator) => new SwitchFrame(locator, false);

        /// <summary>
        /// Constructs the Task object for DefaultContent.
        /// </summary>
        /// <returns></returns>
        public static SwitchFrame ToDefaultContent() => new SwitchFrame(null, true);

        /// <summary>
        /// Constructs the Task object for the given locator without starting from DefaultContent.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <returns></returns>
        public static SwitchFrame WithoutUsingDefaultContentTo(IWebLocator locator) => new SwitchFrame(locator, false, false);

        #endregion

        #region Methods

        /// <summary>
        /// Switches to the frame given by the locator.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            if (StartFromDefaultContent)
                driver.SwitchTo().DefaultContent();

            if (!UseDefaultContent)
            {
                actor.WaitsUntil(Existence.Of(Locator), IsEqualTo.True());
                driver.SwitchTo().Frame(Locator.FindElement(driver));
            }
        }

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        public override bool Equals(object obj) =>
            obj is SwitchFrame frame &&
            Locator.Equals(frame.Locator) &&
            StartFromDefaultContent == frame.StartFromDefaultContent &&
            UseDefaultContent == frame.UseDefaultContent;

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() =>
            HashCode.Combine(GetType(), Locator, StartFromDefaultContent, UseDefaultContent);

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            UseDefaultContent
                ? "switch frame to DefaultContent"
                : $"switch frame to '{Locator.Description}'";

        #endregion
    }
}