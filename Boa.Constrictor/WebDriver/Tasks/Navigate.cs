using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System;

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
        /// Constructs the Task object for the given URL.
        /// </summary>
        /// <param name="url">The target URL.</param>
        /// <returns></returns>
        public static Navigate ToUrl(string url) => new Navigate(url);

        #endregion

        #region Methods

        /// <summary>
        /// Navigates the browser to the target URL.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver) =>
            driver.Navigate().GoToUrl(Url);

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        public override bool Equals(object obj) => 
            obj is Navigate navigate && Url == navigate.Url;

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => HashCode.Combine(GetType(), Url);

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"navigate browser to '{Url}'";

        #endregion
    }
}
