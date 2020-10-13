using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Switches the browser window in focus.
    /// Selenium WebDriver treats tabs like windows, too.
    /// </summary>
    public class SwitchWindow : AbstractWebTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="handle">The window handle.</param>
        private SwitchWindow(string handle) => Handle = handle;

        #endregion

        #region Properties

        /// <summary>
        /// The window handle.
        /// </summary>
        private string Handle { get; set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the task object for the given window handle.
        /// </summary>
        /// <param name="handle">The window handle.</param>
        /// <returns></returns>
        public static SwitchWindow To(string handle) => new SwitchWindow(handle);

        #endregion

        #region Methods

        /// <summary>
        /// Navigates the browser to the target URL.
        /// </summary>
        /// <param name="actor">The screenplay actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver) => driver.SwitchTo().Window(Handle);

        /// <summary>
        /// Returns a description of the task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Switch browser window to '{Handle}'";

        #endregion
    }
}
