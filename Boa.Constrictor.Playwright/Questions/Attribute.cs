namespace Boa.Constrictor.Playwright.Questions
{
    using System.Threading.Tasks;
    using Boa.Constrictor.Playwright.Extensions;
    using Boa.Constrictor.Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Gets an elements attribute value.
    /// </summary>
    public class Attribute : AbstractPageQuestion<string>
    {
        private readonly string Selector;
        private readonly string Name;
        private readonly PageGetAttributeOptions Options;

        #region Constructor

        private Attribute(string selector, string name, PageGetAttributeOptions options)
        {
            Selector = selector;
            Name = name;
            Options = options;
        }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="selector">The target Web element's selector</param>
        /// <param name="name">The name of the target attribute</param>
        /// <param name="options">Playwright options for retrieving attribute values.</param>
        /// <returns></returns>
        public static Attribute Of(string selector, string name, PageGetAttributeOptions options = null)
        {
            return new Attribute(selector, name, options);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an elements attribute value
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="page">The current page.</param>
        /// <returns></returns>
        public override async Task<string> RequestAsAsync(IActor actor, IPage page)
        {
            return await page.GetAttributeAsync(Selector, Name, Options);
        }

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Name} attribute on {Selector}";

        #endregion
    }
}