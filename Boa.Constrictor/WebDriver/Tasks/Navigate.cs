using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Navigates the browser to a specific URL.
    /// </summary>
    public class Navigate : AbstractWebTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="url">The target URL.</param>
        private Navigate(string url) => Url = url;

        #endregion

        #region Properties

        /// <summary>
        /// The target URL.
        /// </summary>
        private string Url { get; set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the task object for the given URL.
        /// </summary>
        /// <param name="url">The target URL.</param>
        /// <returns></returns>
        public static Navigate ToUrl(string url) => new Navigate(url);

        #endregion

        #region Methods

        /// <summary>
        /// Navigates the browser to the target URL.
        /// </summary>
        /// <param name="actor">The screenplay actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver) =>
            driver.Navigate().GoToUrl(Url);

        /// <summary>
        /// Returns a description of the task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Navigate browser to '{Url}'";

        #endregion
    }
}
