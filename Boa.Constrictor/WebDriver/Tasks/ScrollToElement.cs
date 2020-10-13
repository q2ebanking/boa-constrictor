using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Scrolls directly to an element using JavaScript.
    /// </summary>
    public class ScrollToElement : AbstractWebLocatorTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="alignToTop">Aligns scrolling to the top of the target element if true.</param>
        private ScrollToElement(IWebLocator locator, bool alignToTop = true) : base(locator) =>
            AlignToTop = alignToTop;

        #endregion

        #region Properties

        /// <summary>
        /// Aligns scrolling to the top of the target element if true.
        /// </summary>
        public bool AlignToTop { get; }

        /// <summary>
        /// The JavaScript scroll command.
        /// </summary>
        private string ScrollCommand => $"arguments[0].scrollIntoView({AlignToTop.ToString().ToLower()});";

        /// <summary>
        /// ToString adjective.
        /// </summary>
        protected override string ToStringAdjective => "at";

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the task object.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="alignToTop">Aligns scrolling to the top of the target element if true.</param>
        /// <returns></returns>
        public static ScrollToElement At(IWebLocator locator, bool alignToTop = true) =>
            new ScrollToElement(locator, alignToTop);

        #endregion

        #region Methods

        /// <summary>
        /// Scrolls directly to an element using JavaScript.
        /// </summary>
        /// <param name="actor">The screenplay actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver) =>
            actor.AsksFor(JavaScriptElementCall.To(ScrollCommand, Locator));

        /// <summary>
        /// Returns a description of the task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => 
            $"Scroll to element {ToStringAdjective} '{Locator.Description}'";

        #endregion
    }
}
