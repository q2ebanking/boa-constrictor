using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System.Drawing;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets a Web element's location.
    /// </summary>
    public class Location : AbstractWebLocatorQuestion<Point>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private Location(IWebLocator locator) : base(locator) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static Location Of(IWebLocator locator) => new Location(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Gets the Web element's location.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override Point RequestAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Existence.Of(Locator), IsEqualTo.True());
            return driver.FindElement(Locator.Query).Location;
        }

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"location of '{Locator.Description}'";

        #endregion
    }
}
