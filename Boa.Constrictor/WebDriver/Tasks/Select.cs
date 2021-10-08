using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

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
        /// Constructs the Task object to select by index.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="index">The index of the option to select.</param>
        /// <returns></returns>
        public static Select ByIndex(IWebLocator locator, int index) =>
            new Select(locator, index: index);

        /// <summary>
        /// Constructs the Task object to select by text.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="text">The text of the option to select.</param>
        /// <param name="partialMatch">If true, do partial match on option text.</param>
        /// <returns></returns>
        public static Select ByText(IWebLocator locator, string text, bool partialMatch = false) =>
            new Select(locator, text: text, partialMatch: partialMatch);

        /// <summary>
        /// Constructs the Task object to select by value.
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
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Existence.Of(Locator), IsEqualTo.True());

            var select = new SelectElement(driver.FindElement(Locator.Query));

            if (Index != null)
                select.SelectByIndex((int)Index);
            else if (Text != null)
                select.SelectByText(Text, PartialMatch);
            else if (Value != null)
                select.SelectByValue(Value);
            else
                throw new BrowserInteractionException(
                    $"No select method (index, text, or value) provided for Select Task");
        }

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        public override bool Equals(object obj) =>
            obj is Select select &&
            EqualityComparer<IWebLocator>.Default.Equals(Locator, select.Locator) &&
            Index == select.Index &&
            PartialMatch == select.PartialMatch &&
            Text == select.Text &&
            Value == select.Value;

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() =>
            HashCode.Combine(GetType(), Locator, $"Index {Index}", $"PartialMatch {PartialMatch}", $"Text {Text}", $"Value {Value}");

        #endregion
    }
}
