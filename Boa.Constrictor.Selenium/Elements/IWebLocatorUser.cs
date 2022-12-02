namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Adds an IWebLocator property to a class.
    /// </summary>
    public interface IWebLocatorUser
    {
        #region Properties

        /// <summary>
        /// The Web locator.
        /// </summary>
        IWebLocator Locator { get; }

        #endregion
    }
}
