using System;
using Boa.Constrictor.Screenplay;

namespace Boa.Constrictor.Selenium
{
    /// <summary>
    /// Performs a list of tasks within a frame then switches to DefaultContent.
    /// </summary>
    public class PerformInFrame : ITask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="tasks">The Task list.</param>
        private PerformInFrame(IWebLocator locator, ITask[] tasks) 
        {
            Locator = locator;
            Tasks = tasks;  
        }

        #endregion

        #region Properties

        /// <summary>
        /// The locator.
        /// </summary>
        private IWebLocator Locator { get; }

        /// <summary>
        /// The Task list.
        /// </summary>
        private ITask[] Tasks { get; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Builder method.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="tasks">The Task list.</param>
        public static PerformInFrame At(IWebLocator locator, params ITask[] tasks) => 
            new PerformInFrame(locator, tasks);

        #endregion

        #region Methods

        /// <summary>
        /// Runs the Tasks in the order given by the list.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(SwitchFrame.To(Locator));
            actor.AttemptsTo(RunTasks.InOrder(Tasks));
            actor.AttemptsTo(SwitchFrame.ToDefaultContent());
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"run Tasks within the frame '{Locator.Description}'";

        #endregion
    }
}