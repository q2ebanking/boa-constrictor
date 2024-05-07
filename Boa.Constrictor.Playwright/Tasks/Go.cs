namespace Boa.Constrictor.Playwright
{
    using System.Threading.Tasks;
    using Boa.Constrictor.Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Navigates the browser to a specific URL.
    /// </summary>
    public class Go : AbstractPageTask
    {
        private readonly string Url;
        private readonly PageGotoOptions PageGotoOptions;

        #region Constructors

        private Go(string url, PageGotoOptions pageGotoOptions)
        {
            this.Url = url;
            PageGotoOptions = pageGotoOptions;
        }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Task object for the given URL.
        /// </summary>
        /// <param name="url">The target URL.</param>
        /// <param name="options">Call options.</param>
        /// <returns></returns>
        public static Go To(string url, PageGotoOptions options = null) => new Go(url, options);

        #endregion

        #region Methods

        /// <summary>
        /// Navigates the browser to the target URL.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="page">The current Playwright page.</param>
        public override async Task PerformAsAsync(IActor actor, IPage page)
        {
            await page.GotoAsync(this.Url, PageGotoOptions);
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"go to {Url}";

        #endregion

    }
}