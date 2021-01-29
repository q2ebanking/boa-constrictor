using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Gets a web element's "id" attribute.
    /// </summary>
    public class IdAttribute : AbstractWebPropertyQuestion<string>
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
        protected IdAttribute(IWebLocator locator) : base(locator, Id) { }

        #endregion
        
        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static IdAttribute Of(IWebLocator locator) => new IdAttribute(locator);

        #endregion

        #region Methods

        /// <summary>
        /// Gets a web element's HTML attribute by name.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override string RequestAs(IActor actor, IWebDriver driver)
        {
            actor.WaitsUntil(Existence.Of(Locator), IsEqualTo.True());
            return driver.FindElement(Locator.Query).GetAttribute(PropertyName);
        }
        
        #endregion
    }
}
