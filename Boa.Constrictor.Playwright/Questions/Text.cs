namespace Boa.Constrictor.Playwright.Questions
{
    using System.Threading.Tasks;
    using Boa.Constrictor.Playwright.Extensions;
    using Boa.Constrictor.Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Gets the inner text of a web element.
    /// </summary>
    public class Text : AbstractPageQuestion<string>
    {
        private readonly string Locator;

        #region Constructors

        private Text(string locator)
        {
            this.Locator = locator;
        }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target web element's selector.</param>
        /// <returns></returns>
        public static Text Of(string locator)
        {
            return new Text(locator);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the InnerText of an element.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <param name="page">The current page.</param>
        /// <returns></returns>
        public override async Task<string> RequestAsAsync(IActor actor, IPage page)
        {
            return await page.Locator(this.Locator).InnerTextAsync();
        }

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"text of {Locator}";
        }

        #endregion

    }
}