using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Uncheck an element if already selected
    /// Useful for checkboxes or radio buttons
    /// </summary>
    public class Uncheck : AbstractWebLocatorTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private Uncheck(IWebLocator locator) : base(locator) { }

        #endregion
        
        #region Builder Methods

        /// <summary>
        /// Constructs the task object.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static Uncheck On(IWebLocator locator) => new Uncheck(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Uncheck an element if already selected
        /// Useful for checkboxes or radio buttons
        /// </summary>
        /// <param name="actor">The screenplay actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Appearance.Of(Locator), IsEqualTo.True());
            bool isSelected = actor.AsksFor(SelectedState.Of(Locator));

            if (isSelected != false) actor.AttemptsTo(Click.On(Locator));
        }

        #endregion
    }
}
