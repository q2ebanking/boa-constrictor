using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Utilities;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// This is an abstract superclass for any Web questions that use a Web element locator.
    /// </summary>
    /// <typeparam name="TAnswer"></typeparam>
    public abstract class AbstractWebLocatorQuestion<TAnswer> : AbstractWebQuestion<TAnswer>, IWebLocatorUser
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        public AbstractWebLocatorQuestion(IWebLocator locator) => Locator = locator;

        #endregion

        #region Properties

        /// <summary>
        /// The adjective to use for the Locator in the ToString method.
        /// </summary>
        protected virtual string ToStringAdjective => "of";

        /// <summary>
        /// The target Web element's locator.
        /// </summary>
        public IWebLocator Locator { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Asks the question and returns the answer.
        /// Internally calls RequestAs with the WebDriver from the BrowseTheWeb ability.
        /// Internally retries the interaction if StaleElementReferenceException happens.
        /// </summary>
        /// <param name="actor">The screenplay actor.</param>
        /// <returns></returns>
        public override TAnswer RequestAs(IActor actor)
        {
            TAnswer request() => RequestAs(actor, actor.Using<BrowseTheWeb>().WebDriver);
            TAnswer answer = Retries.RetryOnException<StaleElementReferenceException, TAnswer>(request, ToString(), logger: actor.Logger);
            return answer;
        }

        /// <summary>
        /// Returns a description of the question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"{GetType().Name} {ToStringAdjective} '{Locator.Description}'";

        #endregion
    }
}
