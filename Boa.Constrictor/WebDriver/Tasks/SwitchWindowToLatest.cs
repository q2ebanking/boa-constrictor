using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Safely switches to the newest tab in the browser.
    /// </summary>
    public class SwitchWindowToLatest : AbstractWebTask
    {
        #region Constructors


        /// <summary>
        /// Private Constructor.
        /// (Use the public builder methods.)
        /// </summary>
        private SwitchWindowToLatest() { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Task builder.
        /// </summary>
        /// <returns></returns>
        public static SwitchWindowToLatest InBrowser() => new SwitchWindowToLatest();

        #endregion

        #region Methods

        /// <summary>
        /// Gets the latest window handle and switches to it.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver from the BrowseTheWeb Ability.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            string handle = actor.AsksFor(WindowHandle.Latest());
            actor.AttemptsTo(SwitchWindow.To(handle));
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => "switch to the latest browser window";

        #endregion
    }
}
