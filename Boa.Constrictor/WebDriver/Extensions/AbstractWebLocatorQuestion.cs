using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// This is an abstract superclass for any Web Questions that use a Web element locator.
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
        /// Asks the Question and returns the answer.
        /// Internally calls RequestAs with the WebDriver from the BrowseTheWeb Ability.
        /// Internally retries the interaction if StaleElementReferenceException happens.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        public override TAnswer RequestAs(IActor actor)
        {
            TAnswer request() => RequestAs(actor, actor.Using<BrowseTheWeb>().WebDriver);
            TAnswer answer = Retries.RetryOnException<StaleElementReferenceException, TAnswer>(request, ToString(), logger: actor.Logger);
            return answer;
        }

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns></returns>
        public override bool Equals(object obj) =>
            base.Equals(obj) &&
            EqualityComparer<IWebLocator>.Default.Equals(Locator, ((AbstractWebLocatorQuestion<TAnswer>)obj).Locator);

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => 
            HashCode.Combine(GetType(), Locator);

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"{GetType().Name} {ToStringAdjective} '{Locator.Description}'";

        #endregion
    }
}
