using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// The concrete implementation of IWebLocator.
    /// </summary>
    public class WebLocator : IWebLocator
    {
        #region Builder Methods

        /// <summary>
        /// Convenient builder method for constructing WebLocator objects for IDs.
        /// </summary>
        /// <param name="id">The target element ID.</param>
        /// <returns></returns>
        public static WebLocator Id(string id) =>
            new WebLocator($"ID for \"{id}\"", By.Id(id));

        /// <summary>
        /// Convenient builder method for constructing WebLocator objects without too much text.
        /// </summary>
        /// <param name="description">Plain-language description of the Web element (used for logging).</param>
        /// <param name="query">Query for the Web element.</param>
        /// <returns></returns>
        public static WebLocator L(string description, By query) =>
            new WebLocator(description, query);

        /// <summary>
        /// Convenient builder method for constructing WebLocator objects for links.
        /// </summary>
        /// <param name="linkText">The link text.</param>
        /// <returns></returns>
        public static WebLocator Link(string linkText) =>
            new WebLocator($"Link for \"{linkText}\"", By.LinkText(linkText));

        /// <summary>
        /// Convenient builder method for constructing WebLocator objects for links using partial (not full) text.
        /// </summary>
        /// <param name="linkText">The link text.</param>
        /// <returns></returns>
        public static WebLocator PartialLink(string linkText) =>
            new WebLocator($"Partial Link for \"{linkText}\"", By.PartialLinkText(linkText));

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="description">Plain-language description of the Web element (used for logging).</param>
        /// <param name="query">Query for the Web element.</param>
        public WebLocator(string description, By query)
        {
            Description = description;
            Query = query;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Plain-language description of the Web element (used for logging).
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Query for the Web element.
        /// </summary>
        public By Query { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"'{Description}' ({Query})";

        #endregion
    }
}
