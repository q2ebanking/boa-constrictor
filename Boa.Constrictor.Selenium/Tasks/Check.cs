using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Checks an element if not already selected
    /// Useful for checkboxes or radio buttons
    /// </summary>
    public class Check : AbstractWebLocatorTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="checkState">flag indicating if the target element should be checked or unchecked</param>
        private Check(IWebLocator locator, bool checkState) : base(locator)
        {
            CheckState = checkState;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Flag indicating if the target element should be checked or unchecked
        /// </summary>
        private bool CheckState { get; set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Task object.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static Check On(IWebLocator locator) => new Check(locator, true);

        /// <summary>
        /// Constructs the Task object.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static Check Off(IWebLocator locator) => new Check(locator, false);

        #endregion

        #region Methods

        /// <summary>
        /// Checks an element if not already selected
        /// Useful for checkboxes or radio buttons
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Appearance.Of(Locator), IsEqualTo.True());
            bool selectedState = actor.AsksFor(SelectedState.Of(Locator));

            if (selectedState != CheckState) actor.AttemptsTo(Click.On(Locator));
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var action = CheckState == true ? "on" : "off";
            return $"check {action} {Locator.Description}";
        }

        #endregion
    }
}
