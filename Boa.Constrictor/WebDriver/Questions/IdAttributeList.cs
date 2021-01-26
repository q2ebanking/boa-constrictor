namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets a list of Web elements' id values.
    /// Useful when working with a group of Web elements whose ids are unique, but not consistent.
    /// </summary>
    public class IdAttributeList : HtmlAttributeList
    {
        #region Constants

        /// <summary>
        /// The "id" attribute name.
        /// </summary>
        public const string Id = "id";

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        private IdAttributeList(IWebLocator locator) : base(locator, Id) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static IdAttributeList For(IWebLocator locator) => new IdAttributeList(locator);

        #endregion
    }
}
