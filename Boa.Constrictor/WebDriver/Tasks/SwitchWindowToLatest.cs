using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;

namespace Boa.ProductSupport.PrecisionLender.Screenplay
{
    /// <summary>
    /// Safely switches to the newest tab in the browser.
    /// </summary>
    public class SwitchWindowToLatest : ITask
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
        /// <param name="actor">The Screenplay actor.</param>
        public void PerformAs(IActor actor)
        {
            string handle = actor.AsksFor(WindowHandle.Latest());
            actor.AttemptsTo(SwitchWindow.To(handle));
        }

        /// <summary>
        /// Returns a description of the task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Switch to the latest browser window";

        #endregion
    }
}
