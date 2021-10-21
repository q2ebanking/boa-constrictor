using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System.Drawing;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets the size of the web element.
    /// </summary>
    public class PixelSize : AbstractWebLocatorQuestion<Size>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private PixelSize(IWebLocator locator) : base(locator) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static PixelSize Of(IWebLocator locator) => new PixelSize(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Gets the size of the web element.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override Size RequestAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Existence.Of(Locator), IsEqualTo.True());
            return driver.FindElement(Locator.Query).Size;
        }

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"pixel size of '{Locator.Description}'";

        #endregion
    }
}
