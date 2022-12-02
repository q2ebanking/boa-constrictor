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
    public class SystemNetCookie : ICacheableQuestion<System.Net.Cookie>
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
        /// Constructs the Question.
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
        /// <param name="span">The time span by which to increase the expiration.</param>
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
        /// <param name="actor">The Screenplay Actor.</param>
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
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <returns></returns>
        public override bool Equals(object obj) =>
            obj is SystemNetCookie cookie &&
            CookieName == cookie.CookieName &&
            Expiration == cookie.Expiration;

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => HashCode.Combine(CookieName, Expiration);

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string message = $"System.Net.Cookie named '{CookieName}'";

            if (Expiration != null)
                message += $" and reset expiration to '{Expiration}'";

            return message;
        }

        #endregion
    }
}
