using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Submits a form.
    /// Submit may be called on any element in the form.
    /// This may be more convenient than explicitly searching for the form's submit input.
    /// </summary>
    public class Submit : AbstractWebLocatorTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private Submit(IWebLocator locator) : base(locator) { }

        #endregion
        
        #region Builder Methods

        /// <summary>
        /// Constructs the Task object.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static Submit On(IWebLocator locator) => new Submit(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Submits a form.
        /// Submit may be called on any element in the form.
        /// This may be more convenient than explicitly searching for the form's submit input.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Existence.Of(Locator), IsEqualTo.True());
            driver.FindElement(Locator.Query).Submit();
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => "submit a form";

        #endregion
    }
}
