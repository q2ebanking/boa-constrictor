using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// An interface for locating Web elements on a page.
    /// </summary>
    public interface IWebLocator
    {
        #region Properties

        /// <summary>
        /// Plain-language description of the Web element (used for logging).
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Query for the Web element.
        /// </summary>
        By Query { get; }

        #endregion
    }
}
