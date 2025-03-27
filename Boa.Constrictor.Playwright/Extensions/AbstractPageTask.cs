namespace Boa.Constrictor.Playwright
{
    using System;
    using System.Threading.Tasks;
    using Boa.Constrictor.Screenplay;
    using Microsoft.Playwright;

    /// <summary>
    /// Abstract class that makes it easier to write Tasks that use the BrowseTheWebWithPlaywright Ability.
    /// </summary>
    public abstract class AbstractPageTask : ITaskAsync
    {
        #region Abstract Methods

        /// <summary>
        /// Performs the Task.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="page">The CurrentPage from the BrowseTheWebWithPlaywright Ability.</param>
        public abstract Task PerformAsAsync(IActor actor, IPage page);

        #endregion

        #region Methods

        /// <summary>
        /// Performs the Task.
        /// Internally calls PerformAsAsync with the CurrentPage from the BrowseTheWebWithPlaywright Ability.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        public virtual async Task PerformAsAsync(IActor actor) => await PerformAsAsync(actor, await actor.Using<BrowseTheWebWithPlaywright>().GetCurrentPageAsync());

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
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => GetType().Name;

        #endregion



    }
}