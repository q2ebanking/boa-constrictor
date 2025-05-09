namespace Boa.Constrictor.Playwright
{
    using System;
    using System.Threading.Tasks;
    using Boa.Constrictor.Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Abstract class that makes it easier to write Questions that use the BrowseTheWebWithPlaywright Ability.
    /// </summary>
    /// <typeparam name="TAnswer">The answer type.</typeparam>
    public abstract class AbstractPageQuestion<TAnswer> : IQuestionAsync<TAnswer>
    {
        #region Abstract Methods

        /// <summary>
        /// Asks the Question and returns the answer.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="page">The CurrentPage from the BrowseTheWebWithPlaywright Ability.</param>
        /// <returns></returns>
        public abstract Task<TAnswer> RequestAsAsync(IActor actor, IPage page);

        #endregion

        #region Methods

        /// <summary>
        /// Asks the Question and returns the answer.
        /// Internally calls RequestAs with the WebDriver from the BrowseTheWeb Ability.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        public virtual async Task<TAnswer> RequestAsAsync(IActor actor) => await RequestAsAsync(actor, await actor.Using<BrowseTheWebWithPlaywright>().GetCurrentPageAsync());

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns></returns>
        public override bool Equals(object obj) => obj?.GetType() == GetType();

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => HashCode.Combine(GetType());

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => GetType().Name;

        #endregion
    }
}