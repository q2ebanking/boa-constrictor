namespace Boa.Constrictor.Playwright
{
    using System.Threading.Tasks;
    using Boa.Constrictor.Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Sets the checked state of an element.
    /// </summary>
    public class SetChecked : AbstractLocatorTask
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="locator">The target element.</param>
        /// <param name="checkedState">The checked state.</param>
        /// <param name="options">Call options.</param>
        private SetChecked(IPlaywrightLocator locator, bool checkedState, LocatorSetCheckedOptions options) : base(locator)
        {
            CheckedState = checkedState;
            Options = options;
        }

        #endregion

        #region Properties

        private bool CheckedState { get; }
        private LocatorSetCheckedOptions Options { get; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Toggles the check state to on.
        /// </summary>
        /// <param name="locator">The target element.</param>
        /// <param name="options">Call options.</param>
        /// <returns></returns>
        public static SetChecked On(IPlaywrightLocator locator, LocatorSetCheckedOptions options = null)
        {
            return new SetChecked(locator, true, options);
        }

        /// <summary>
        /// Toggles the check state to off.
        /// </summary>
        /// <param name="locator">The target element.</param>
        /// <param name="options">Call options.</param>
        /// <returns></returns>
        public static SetChecked Off(IPlaywrightLocator locator, LocatorSetCheckedOptions options = null)
        {
            return new SetChecked(locator, false, options);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the checked state of an element.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="locator">The target locator.</param>
        public override async Task PerformAsAsync(IActor actor, ILocator locator)
        {
            await locator.SetCheckedAsync(CheckedState, Options);
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Set checked state to {CheckedState} on {Locator.Description}";
        }

        #endregion

    }
}