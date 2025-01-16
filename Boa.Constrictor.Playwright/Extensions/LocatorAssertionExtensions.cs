namespace Boa.Constrictor.Playwright
{
    using Boa.Constrictor.Playwright.Abilities;
    using Boa.Constrictor.Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Provides IActor extension methods to simplify assertions using playwright.
    ///
    /// </summary>
    public static class LocatorAssertionExtensions
    {
        /// <summary>
        /// Exposes playwrights ILocatorAssertions
        /// Calls Assertions.Expect()
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="locator">locator to perform assertions on.</param>
        /// <returns></returns>
        public static ILocatorAssertions Expects(this IActor actor, IPlaywrightLocator locator)
        {
            var page = actor.Using<BrowseTheWebSynchronously>().CurrentPage;
            var element = locator.FindIn(page);
            return Assertions.Expect(element);
        }
        
        /// <summary>
        /// Exposes playwrights IPageAssertions
        /// Calls Assertions.Expect()
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <returns></returns>
        public static IPageAssertions ExpectsPage(this IActor actor)
        {
            var page = actor.Using<BrowseTheWebSynchronously>().CurrentPage;
            return Assertions.Expect(page);
        }
    }
}