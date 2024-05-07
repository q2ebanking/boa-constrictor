namespace Boa.Constrictor.Playwright
{
    using Microsoft.Playwright;

    /// <summary>
    /// An interface for finding locators on a page.
    /// </summary>
    public interface IPlaywrightLocator
    {
        /// <summary>
        /// Plain-language description of the Locator (used for logging)
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Finds the locator in the specified page.
        /// </summary>
        /// <param name="page">The page to search for this locator.</param>
        /// <returns></returns>
        ILocator FindIn(IPage page);
    }
}