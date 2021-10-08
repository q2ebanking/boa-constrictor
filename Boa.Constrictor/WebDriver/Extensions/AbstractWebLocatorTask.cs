using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Abstract class for any Web Tasks that use a Web element locator.
    /// </summary>
    public abstract class AbstractWebLocatorTask : AbstractWebTask, IWebLocatorUser
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        public AbstractWebLocatorTask(IWebLocator locator) => Locator = locator;

        #endregion

        #region Properties

        /// <summary>
        /// The adjective to use for the Locator in the ToString method.
        /// </summary>
        protected virtual string ToStringAdjective => "on";

        /// <summary>
        /// The target Web element's locator.
        /// </summary>
        public IWebLocator Locator { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Attempts the Task.
        /// Internally calls RequestAs with the WebDriver from the BrowseTheWeb Ability.
        /// Internally retries the interaction if StaleElementReferenceException happens.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        public override void PerformAs(IActor actor)
        {
            bool attempt()
            {
                PerformAs(actor, actor.Using<BrowseTheWeb>().WebDriver);
                return true;
            }
                
            Retries.RetryOnException<StaleElementReferenceException, bool>(attempt, ToString(), logger: actor.Logger);
        }

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns></returns>
        public override bool Equals(object obj) =>
            base.Equals(obj) &&
            EqualityComparer<IWebLocator>.Default.Equals(Locator, ((AbstractWebLocatorTask)obj).Locator);

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() =>
            HashCode.Combine(GetType(), Locator);

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"{GetType().Name} {ToStringAdjective} '{Locator.Description}'";

        #endregion
    }
}
