using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets the current WebDriver URL.
    /// </summary>
    public class CurrentUrl : AbstractWebQuestion<string>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        private CurrentUrl() { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <returns></returns>
        public static CurrentUrl FromBrowser() => new CurrentUrl();

        #endregion

        #region Methods

        /// <summary>
        /// Gets the current WebDriver URL.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override string RequestAs(IActor actor, IWebDriver driver) => driver.Url;

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => "current browser URL";

        #endregion
    }
}
