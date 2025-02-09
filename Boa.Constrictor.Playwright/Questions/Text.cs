namespace Boa.Constrictor.Playwright
{
    using System.Threading.Tasks;
    using Boa.Constrictor.Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Gets the inner text of a web element.
    /// </summary>
    public class Text : AbstractLocatorQuestion<string>
    {
        #region Constructors

        private Text(IPlaywrightLocator locator)
        :base(locator)
        {
        }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target web element's selector.</param>
        /// <returns></returns>
        public static Text Of(IPlaywrightLocator locator)
        {
            return new Text(locator);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the InnerText of an element.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <param name="locator">The current page.</param>
        /// <returns></returns>
        public override async Task<string> RequestAsAsync(IActor actor, ILocator locator)
        {
            return await locator.InnerTextAsync();
        }

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"text of {Locator.Description}";
        }

        #endregion
    }
}