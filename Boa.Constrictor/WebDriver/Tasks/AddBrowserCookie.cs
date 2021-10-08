using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Adds a cookie to the browser through the WebDriver instance.
    /// </summary>
    public class AddBrowserCookie : AbstractWebTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use public builder methods instead.)
        /// </summary>
        /// <param name="cookie">The cookie to add to the WebDriver.</param>
        private AddBrowserCookie(OpenQA.Selenium.Cookie cookie) => Cookie = cookie;

        #endregion

        #region Properties

        /// <summary>
        /// The cookie to add to the WebDriver.
        /// </summary>
        private OpenQA.Selenium.Cookie Cookie { get; set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Creates the Task.
        /// </summary>
        /// <param name="cookie">The cookie to add to the WebDriver.</param>
        /// <returns></returns>
        public static AddBrowserCookie Named(OpenQA.Selenium.Cookie cookie) =>
            new AddBrowserCookie(cookie);

        /// <summary>
        /// Creates the Task.
        /// </summary>
        /// <param name="name">The cookie name.</param>
        /// <param name="value">The cookie value.</param>
        /// <returns></returns>
        public static AddBrowserCookie Named(string name, string value) =>
            Named(new OpenQA.Selenium.Cookie(name, value));

        /// <summary>
        /// Creates the Task.
        /// </summary>
        /// <param name="cookie">The cookie to add to the WebDriver.</param>
        /// <returns></returns>
        public static AddBrowserCookie Named(System.Net.Cookie cookie) =>
            Named(cookie.Name, cookie.Value);

        #endregion

        #region Methods

        /// <summary>
        /// Adds the cookie to the WebDriver.
        /// Sometimes, adding the cookie fails, so keep retrying until it works.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            actor.Logger.Info($"Cookie: {Cookie.Name} = {Cookie.Value}");
            driver.Manage().Cookies.AddCookie(Cookie);
        }

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns></returns>
        public override bool Equals(object obj) =>
            obj is AddBrowserCookie cookie &&
            EqualityComparer<Cookie>.Default.Equals(Cookie, cookie.Cookie);

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// Warning: Cookies with the same names but different values have the same hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => HashCode.Combine(GetType(), Cookie);

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"add a cookie named '{Cookie.Name}' to the browser";

        #endregion
    }
}
