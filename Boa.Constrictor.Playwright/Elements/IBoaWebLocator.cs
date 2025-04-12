using OpenQA.Selenium;

namespace Boa.Constrictor.Playwright
{
    /// <summary>
    /// Interface for BoaWebLocator utilities.
    /// </summary>
    public interface IBoaWebLocator
    {
        /// <summary>
        /// Creates a BoaWebLocator instance with the specified query and description.
        /// </summary>
        /// <param name="query">The Selenium By query.</param>
        /// <param name="description">The description of the locator.</param>
        /// <returns>A new BoaWebLocator instance.</returns>
        BoaWebLocator L(By query, string description = null);
    }
}