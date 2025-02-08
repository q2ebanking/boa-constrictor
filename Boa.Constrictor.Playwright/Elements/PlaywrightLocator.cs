namespace Boa.Constrictor.Playwright
{
    using System;
    using Microsoft.Playwright;

    /// <summary>
    /// The concrete implementation of IPlaywrightLocator.
    /// </summary>
    public class PlaywrightLocator : IPlaywrightLocator
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="description">Plain-language description of the Locator (used for logging).</param>
        /// <param name="selectorFunc">A function to find a locator on a page.</param>
        public PlaywrightLocator(string description, Func<IPage, ILocator> selectorFunc)
        {
            Description = description;
            SelectorFunc = selectorFunc;
        }

        /// <summary>
        /// Plain-language description of the Locator (used for logging)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A function to find a locator on a page.
        /// </summary>
        private Func<IPage, ILocator> SelectorFunc { get; set; }


        /// <summary>
        /// Convenient builder method for construction PlaywrightLocator objects without too much text.
        /// </summary>
        /// <param name="description">Plain-language description of the Locator (used for logging).</param>
        /// <param name="selectorFunc">A function to find a locator on a page.</param>
        /// <returns></returns>
        public static PlaywrightLocator L(string description, Func<IPage, ILocator> selectorFunc)
        {
            return new PlaywrightLocator(description, selectorFunc);
        }

        /// <summary>
        /// Convenient builder method for construction PlaywrightLocator objects without too much text.
        /// </summary>
        /// <param name="description">Plain-language description of the Locator (used for logging).</param>
        /// <param name="selector">selector used to resolve DOM elements</param>
        /// <param name="options">Call options</param>
        /// <returns></returns>
        public static PlaywrightLocator L(string description, string selector, PageLocatorOptions options = null)
        {
            return new PlaywrightLocator(description, page => page.Locator(selector, options));
        }

        /// <summary>
        /// Finds the locator in the specified page.
        /// </summary>
        /// <param name="page">The page to search for this locator.</param>
        /// <returns></returns>
        public ILocator FindIn(IPage page)
        {
            return SelectorFunc.Invoke(page);
        }
    }
}