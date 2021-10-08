using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Abstract class that makes it easier to write Tasks that use the BrowseTheWeb Ability.
    /// </summary>
    public abstract class AbstractWebTask : ITask
    {
        #region Abstract Methods

        /// <summary>
        /// Performs the Task.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver from the BrowseTheWeb Ability.</param>
        public abstract void PerformAs(IActor actor, IWebDriver driver);

        #endregion

        #region Methods

        /// <summary>
        /// Performs the Task.
        /// Internally calls PerformAs with the WebDriver from the BrowseTheWeb Ability.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        public virtual void PerformAs(IActor actor) => PerformAs(actor, actor.Using<BrowseTheWeb>().WebDriver);

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
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => GetType().Name;

        #endregion
    }
}
