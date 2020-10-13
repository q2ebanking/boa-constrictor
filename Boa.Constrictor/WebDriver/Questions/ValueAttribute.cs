namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets a web element's "value" HTML attribute.
    /// It is particularly useful for getting text from "input" elements.
    /// </summary>
    public class ValueAttribute : HtmlAttribute
    {
        #region Constants

        /// <summary>
        /// The "value" attribute name.
        /// </summary>
        public const string Value = "value";

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private ValueAttribute(IWebLocator locator) : base(locator, Value) { }

        #endregion
        
        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static ValueAttribute Of(IWebLocator locator) => new ValueAttribute(locator);

        #endregion
    }
}
