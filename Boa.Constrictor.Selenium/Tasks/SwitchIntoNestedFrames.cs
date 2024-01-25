using Boa.Constrictor.Screenplay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.Selenium
{
    /// <summary>
    /// Traverses through nested frames to switch to the final one.
    /// </summary>
    public class SwitchIntoNestedFrames : ITask
    {
        #region Constructors

        /// <summary>
        /// Private Constructor.
        /// (Use the public builder methods.)
        /// </summary>
        /// <param name="locators">The frame locator list.</param>
        private SwitchIntoNestedFrames(List<IWebLocator> locators) => Locators = locators;

        #endregion

        #region Properties

        /// <summary>
        /// The list of frame locators.
        /// The target Web element's locator is the last element of the frame locator list.
        /// </summary>
        public List<IWebLocator> Locators { get; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Task.
        /// </summary>
        /// <param name="locators">The frame locator list.</param>
        public static SwitchIntoNestedFrames To(List<IWebLocator> locators) => new SwitchIntoNestedFrames(locators);

        #endregion

        #region Methods

        /// <summary>
        /// Switch into frames to the last locator in the list.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(SwitchFrame.ToDefaultContent());

            foreach (IWebLocator locator in Locators)
                actor.AttemptsTo(SwitchFrame.WithoutUsingDefaultContentTo(locator));
        }

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        public override bool Equals(object obj) =>
            obj is SwitchIntoNestedFrames frame &&
            Locators.Equals(frame.Locators);

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() =>
            HashCode.Combine(GetType(), Locators);

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"switch into nested frames to '{Locators.Last().Description}'";

        #endregion
    }
}
