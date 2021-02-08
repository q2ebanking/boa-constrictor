using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Abstract class that makes it easier to write questions that use the BrowseTheWeb ability.
    /// </summary>
    /// <typeparam name="TAnswer">The answer type.</typeparam>
    public abstract class AbstractWebQuestion<TAnswer> : ICacheableQuestion<TAnswer>
    {
        #region Abstract Methods

        /// <summary>
        /// Asks the question and returns the answer.
        /// </summary>
        /// <param name="actor">The screenplay actor.</param>
        /// <param name="driver">The WebDriver from the BrowseTheWeb ability.</param>
        /// <returns></returns>
        public abstract TAnswer RequestAs(IActor actor, IWebDriver driver);

        #endregion

        #region Methods

        /// <summary>
        /// Asks the question and returns the answer.
        /// Internally calls RequestAs with the WebDriver from the BrowseTheWeb ability.
        /// </summary>
        /// <param name="actor">The screenplay actor.</param>
        /// <returns></returns>
        public virtual TAnswer RequestAs(IActor actor) => RequestAs(actor, actor.Using<BrowseTheWeb>().WebDriver);

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns></returns>
        public override bool Equals(object obj) => obj.GetType().Equals(GetType());

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => HashCode.Combine(GetType());

        /// <summary>
        /// Returns a description of the question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => GetType().Name;

        #endregion
    }
}
