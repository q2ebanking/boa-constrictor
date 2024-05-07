namespace Boa.Constrictor.Playwright
{
    using System.Threading.Tasks;
    using Boa.Constrictor.Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Gets an elements attribute value.
    /// </summary>
    public class Attribute : AbstractPageQuestion<string>
    {
        private readonly IPlaywrightLocator Locator;
        private readonly string Name;
        private readonly LocatorGetAttributeOptions Options;

        #region Constructor

        private Attribute(IPlaywrightLocator locator, string name, LocatorGetAttributeOptions options)
        {
            Locator = locator;
            Name = name;
            Options = options;
        }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="locator">The target locator.</param>
        /// <param name="name">The name of the target attribute</param>
        /// <param name="options">Playwright options for retrieving attribute values.</param>
        /// <returns></returns>
        public static Attribute Of(IPlaywrightLocator locator, string name, LocatorGetAttributeOptions options = null)
        {
            return new Attribute(locator, name, options);
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
            var locator = this.Locator.FindIn(page);
            return await locator.GetAttributeAsync(Name, Options);
        }

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Name} attribute on {Locator}";

        #endregion
    }
}