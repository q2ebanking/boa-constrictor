namespace Boa.Constrictor.Playwright
{
    using System.Threading.Tasks;
    using Boa.Constrictor.Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Selects option in a <select/> element.
    /// </summary>
    public class SelectOption : AbstractLocatorTask
    {
        #region Constructors

        private SelectOption(IPlaywrightLocator locator, SelectOptionValue optionToSelect, LocatorSelectOptionOptions locatorOptions) : base(locator)
        {
            OptionToSelect = optionToSelect;
            LocatorOptions = locatorOptions;
        }

        #endregion

        #region Properties

        private SelectOptionValue OptionToSelect { get; }
        private LocatorSelectOptionOptions LocatorOptions { get; }

        #endregion

        #region Builder Methods
        /// <summary>
        /// Selects option in a <select/> element by index.
        /// </summary>
        /// <param name="locator">The target locator.</param>
        /// <param name="index">index to be selected.</param>
        /// <param name="locatorOptions">Call Options.</param>
        /// <returns></returns>
        public static SelectOption ByIndex(IPlaywrightLocator locator, int index, LocatorSelectOptionOptions locatorOptions = null)
        {
            var selectOptionValue = new SelectOptionValue()
            {
                Index = index,
            };

            return new SelectOption(locator, selectOptionValue, locatorOptions);
        }

        /// <summary>
        /// Selects option in a <select/> element by label.
        /// </summary>
        /// <param name="locator">The target locator.</param>
        /// <param name="label">label to be selected.</param>
        /// <param name="locatorOptions">Call Options.</param>
        /// <returns></returns>
        public static SelectOption ByLabel(IPlaywrightLocator locator, string label, LocatorSelectOptionOptions locatorOptions = null)
        {
            var selectOptionValue = new SelectOptionValue()
            {
                Label = label,
            };

            return new SelectOption(locator, selectOptionValue, locatorOptions);
        }

        /// <summary>
        /// Selects option in a <select/> element by value.
        /// </summary>
        /// <param name="locator">The target locator.</param>
        /// <param name="value">value to be selected.</param>
        /// <param name="locatorOptions">Call Options.</param>
        /// <returns></returns>
        public static SelectOption ByValue(IPlaywrightLocator locator, string value, LocatorSelectOptionOptions locatorOptions = null)
        {
            var selectOptionValue = new SelectOptionValue()
            {
                Value = value,
            };

            return new SelectOption(locator, selectOptionValue, locatorOptions);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Selects an option from a <select/> element.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="locator">The target locator.</param>
        public override async Task PerformAsAsync(IActor actor, ILocator locator)
        {
            await locator.SelectOptionAsync(OptionToSelect, LocatorOptions);
        }

        #endregion
    }
}