namespace Boa.Constrictor.Playwright.Tasks
{
    using System.Threading.Tasks;
    using Boa.Constrictor.Playwright.Abilities;
    using Boa.Constrictor.Playwright.Extensions;
    using Boa.Constrictor.Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Inputs text in an element
    /// </summary>
    public class Fill : AbstractPageTask
    {
        private readonly string Value;
        private readonly string Selector;

        #region Constructors

        private Fill(string selector, string value)
        {
            this.Selector = selector;
            this.Value = value;
        }

        #endregion

        #region Builder Methods
        /// <summary>
        /// Constructs the Task object.
        /// </summary>
        /// <param name="selector">The target Web element's selector.</param>
        /// <param name="value">The value to be filled(e.g., text).</param>
        /// <returns></returns>
        public static Fill ValueTo(string selector, string value) => new Fill(selector, value);

        #endregion

        #region Methods

        /// <summary>
        /// Inputs text in an element
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="page">The current Playwright page.</param>
        public override async Task PerformAsAsync(IActor actor, IPage page)
        {
            await page.Locator(this.Selector).FillAsync(this.Value);
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"fill the text {Value} into {Selector}";
        }

        #endregion
    }
}