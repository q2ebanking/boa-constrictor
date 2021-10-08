using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Drags the mouse from one element to another
    /// </summary>
    public class Drag : AbstractWebLocatorTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="source">The source web element locator</param>
        /// <param name="target">The target web element locator</param>
        private Drag(IWebLocator source, IWebLocator target) : base(source) 
        {
            Target = target;
        }

        #endregion

        #region Properties
        /// <summary>
        /// The target web element locator
        /// </summary>
        public IWebLocator Target { get; set; }
        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Task object.
        /// </summary>
        /// <param name="source">The source web element locator</param>
        /// <param name="target">The target web element locator</param>
        /// <returns></returns>
        public static Drag AndDrop(IWebLocator source, IWebLocator target) => new Drag(source, target);

        #endregion

        #region Methods

        /// <summary>
        /// Drags the mouse from one element to another
        /// Use browser actions instead of direct click (due to IE).
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Appearance.Of(Locator), IsEqualTo.True());
            actor.WaitsUntil(Appearance.Of(Target), IsEqualTo.True());
            new Actions(driver).DragAndDrop(driver.FindElement(Locator.Query), driver.FindElement(Target.Query)).Perform();
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"drag the mouse from '{Locator.Description}' to '{Target.Description}'";

        #endregion
    }
}
