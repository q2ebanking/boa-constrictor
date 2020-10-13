using Boa.Constrictor.Screenplay;
using System;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets the desired cookie from the WebDriver as a System.Net.Cookie.
    /// Internally calls BrowserCookie and converts the internal OpenQA.Selenium.Cookie.
    /// Optionally reset the cookie's expiration.
    /// Warning: The cookie's expiration will be wrong.
    /// </summary>
    public class SystemNetCookie : IQuestion<System.Net.Cookie>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="cookieName">The name of the desired cookie.</param>
        private SystemNetCookie(string cookieName)
        {
            CookieName = cookieName;
            Expiration = null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The name of the desired cookie.
        /// </summary>
        private string CookieName { get; set; }

        /// <summary>
        /// If not null, resets the cookie expiration.
        /// </summary>
        private DateTime? Expiration { get; set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="cookieName">The name of the desired cookie.</param>
        /// <returns></returns>
        public static SystemNetCookie Named(string cookieName) => new SystemNetCookie(cookieName);

        /// <summary>
        /// Resets the cookie's expiration.
        /// </summary>
        /// <param name="expiration">The expiration date and time.</param>
        /// <returns></returns>
        public SystemNetCookie AndResetExpirationTo(DateTime expiration)
        {
            Expiration = expiration;
            return this;
        }

        /// <summary>
        /// Resets the cookie's expiration relative to the current date and time.
        /// </summary>
        /// <param name="expiration">The expiration date and time.</param>
        /// <returns></returns>
        public SystemNetCookie AndResetFutureExpirationTo(TimeSpan span)
        {
            Expiration = DateTime.UtcNow.Add(span);
            return this;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the desired cookie from the WebDriver as a System.Net.Cookie.
        /// Internally calls BrowserCookie and converts the internal OpenQA.Selenium.Cookie.
        /// Optionally reset the cookie's expiration.
        /// Warning: The cookie's expiration will be wrong.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <returns></returns>
        public System.Net.Cookie RequestAs(IActor actor)
        {
            // Get the cookie from WebDriver
            var seCookie = actor.AsksFor(BrowserCookie.Named(CookieName));
            var netCookie = new System.Net.Cookie(seCookie.Name, seCookie.Value, seCookie.Path, seCookie.Domain);

            // Reset the expiration if applicable
            if (Expiration != null)
                netCookie.Expires = (DateTime)Expiration;

            // IE gives cookies a double-slash path, which breaks RestSharp
            if (netCookie.Path == "//")
                netCookie.Path = "/";

            // Return the System.Net.Cookie
            return netCookie;
        }

        /// <summary>
        /// Returns a description of the question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"System.Net.Cookie named '{CookieName}'";

        #endregion
    }
}
