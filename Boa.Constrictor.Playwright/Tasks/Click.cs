namespace Boa.Constrictor.Playwright.Tasks
{
    using System.Threading.Tasks;
    using Boa.Constrictor.Playwright.Abilities;
    using Boa.Constrictor.Playwright.Extensions;
    using Boa.Constrictor.Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Clicks a web element.
    /// </summary>
    public class Click : AbstractPageTask
    {
        private readonly string Selector;

        #region Constructors

        private Click(string selector) => this.Selector = selector;

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Task object.
        /// </summary>
        /// <param name="selector">The target web elements Selector.</param>
        /// <returns></returns>
        public static Click On(string selector) => new Click(selector);

        #endregion

        #region Methods

        /// <summary>
        /// Clicks the web element.
        /// Use browser actions instead of direct click (due to IE).
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="page">The current page.</param>
        public override async Task PerformAsAsync(IActor actor, IPage page)
        {
            await page.ClickAsync(this.Selector);
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"click on {Selector}";

        #endregion
    }
}