using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets the desired OpenQA.Selenium.Cookie from the WebDriver.
    /// Waits for the cookie to be added to the browser if it is not there.
    /// Internally calls Wait for waiting.
    /// </summary>
    public class BrowserCookie : AbstractWebQuestion<Cookie>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="cookieName">The name of the desired cookie.</param>
        private BrowserCookie(string cookieName) => CookieName = cookieName;

        #endregion

        #region Properties

        /// <summary>
        /// The name of the desired cookie.
        /// </summary>
        private string CookieName { get; set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="cookieName">The name of the desired cookie.</param>
        /// <returns></returns>
        public static BrowserCookie Named(string cookieName) => new BrowserCookie(cookieName);

        #endregion

        #region Methods

        /// <summary>
        /// Gets the desired OpenQA.Selenium.Cookie from the WebDriver.
        /// Waits for the cookie to be added to the browser if it is not there.
        /// Internally calls Wait for waiting.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override Cookie RequestAs(IActor actor, IWebDriver driver)
        {
            try
            {
                // Wait for the cookie to exist
                actor.WaitsUntil(BrowserCookieExistence.Named(CookieName), IsEqualTo.True());
            }
            catch (WaitingException<bool> e)
            {
                // Get the cookies that actually exist
                var cookies = driver.Manage().Cookies.AllCookies;

                // Log what cookies were found
                if (cookies.Count == 0)
                {
                    actor.Logger.Info("The browser does not contain any cookies");
                }
                else
                {
                    actor.Logger.Info("The browser contains the following cookies:");
                    foreach (var cookie in cookies)
                        actor.Logger.Info($"{cookie.Name}: {cookie.Value}");
                }

                // Throw an exception for the missing cookie
                throw new BrowserInteractionException($"The browser does not contain a cookie named '{CookieName}'", e);
            }

            // Return the named cookie
            var seCookie = driver.Manage().Cookies.GetCookieNamed(CookieName);
            return seCookie;
        }

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        public override bool Equals(object obj) =>
            obj is BrowserCookie cookie &&
            CookieName == cookie.CookieName;

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() =>
            HashCode.Combine(GetType(), CookieName);

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"browser cookie named '{CookieName}'";

        #endregion
    }
}
