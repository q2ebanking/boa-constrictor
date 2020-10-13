using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Selects an option by value, text, or index in a select element.
    /// </summary>
    public class Select : AbstractWebLocatorTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="index">The index of the option to select. Use if not null.</param>
        /// <param name="partialMatch">If true, do partial match on option text. Only applicable for selection by text.</param>
        /// <param name="text">The text of the option to select. Use if not null.</param>
        /// <param name="value">The text of the option to select. Use if not null.</param>
        private Select(IWebLocator locator, int? index = null, bool partialMatch = false, string text = null, string value = null)
            : base(locator)
        {
            Index = index;
            PartialMatch = partialMatch;
            Text = text;
            Value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The index of the option to select.
        /// Use if not null.
        /// </summary>
        private int? Index { get; }

        /// <summary>
        /// If true, do partial match on option text.
        /// Only applicable for selection by text.
        /// </summary>
        private bool PartialMatch { get; }

        /// <summary>
        /// The text of the option to select.
        /// Use if not null.
        /// </summary>
        private string Text { get; }

        /// <summary>
        /// The value of the option to select.
        /// Use if not null.
        /// </summary>
        private string Value { get; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the task object to select by index.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="index">The index of the option to select.</param>
        /// <returns></returns>
        public static Select ByIndex(IWebLocator locator, int index) =>
            new Select(locator, index: index);

        /// <summary>
        /// Constructs the task object to select by text.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="text">The text of the option to select.</param>
        /// <param name="partialMatch">If true, do partial match on option text.</param>
        /// <returns></returns>
        public static Select ByText(IWebLocator locator, string text, bool partialMatch = false) =>
            new Select(locator, text: text, partialMatch: partialMatch);

        /// <summary>
        /// Constructs the task object to select by value.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="value">The value of the option to select.</param>
        /// <returns></returns>
        public static Select ByValue(IWebLocator locator, string value) =>
            new Select(locator, value: value);

        #endregion

        #region Methods

        /// <summary>
        /// Selects an option by text in a select element.
        /// </summary>
        /// <param name="actor">The screenplay actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            actor.AttemptsTo(Wait.Until(Existence.Of(Locator), IsEqualTo.True()));

            var select = new SelectElement(driver.FindElement(Locator.Query));

            if (Index != null)
                select.SelectByIndex((int)Index);
            else if (Text != null)
                select.SelectByText(Text, PartialMatch);
            else if (Value != null)
                select.SelectByValue(Value);
            else
                throw new BrowserInteractionException(
                    $"No select method (index, text, or value) provided for Select task");
        }

        #endregion
    }
}
