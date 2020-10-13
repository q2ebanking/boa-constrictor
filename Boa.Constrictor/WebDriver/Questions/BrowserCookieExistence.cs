using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Checks if the browser cookie exists.
    /// </summary>
    public class BrowserCookieExistence : AbstractWebQuestion<bool>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="cookieName">The name of the desired cookie.</param>
        private BrowserCookieExistence(string cookieName) => CookieName = cookieName;

        #endregion

        #region Properties

        /// <summary>
        /// The name of the desired cookie.
        /// </summary>
        private string CookieName { get; set; }
        
        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="cookieName">The name of the desired cookie.</param>
        /// <returns></returns>
        public static BrowserCookieExistence Named(string cookieName) => new BrowserCookieExistence(cookieName);

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the browser cookie exists.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override bool RequestAs(IActor actor, IWebDriver driver)
        {
            var seCookie = driver.Manage().Cookies.GetCookieNamed(CookieName);
            return seCookie != null;
        }

        /// <summary>
        /// Returns a description of the question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Existence of browser cookie named '{CookieName}'";

        #endregion
    }
}
