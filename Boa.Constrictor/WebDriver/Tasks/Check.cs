using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

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
        private Check(IWebLocator locator) : base(locator) { }

        #endregion
        
        #region Builder Methods

        /// <summary>
        /// Constructs the task object.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static Check On(IWebLocator locator) => new Check(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Checks an element if not already selected
        /// Useful for checkboxes or radio buttons
        /// </summary>
        /// <param name="actor">The screenplay actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Appearance.Of(Locator), IsEqualTo.True());
            bool isSelected = actor.AsksFor(SelectedState.Of(Locator));

            if (isSelected != true) actor.AttemptsTo(Click.On(Locator));
        }

        #endregion
    }
}
